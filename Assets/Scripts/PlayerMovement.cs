using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public CapsuleCollider2D hitbox;
    
    [SerializeField] private float movespeed;
    [SerializeField] private float jumpspeed;

    private Vector2 direction; // movement direction
    private bool can_double_jump = false;
    private bool try_jump = false; // is the player trying to jump?

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<CapsuleCollider2D>();
        // get reference to components, allows us to use built-in functions
    }

    void Update()
    {   // detect input in Update() so we don't miss any inputs
        
        float x_component = Input.GetAxisRaw("Horizontal");
        float y_component = Input.GetAxisRaw("Vertical");

        direction = new Vector2(x_component * movespeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump")) {
            // if we press the jump button, remember that the player wants to jump
            // we need to save this as a variable so it gets processed in FixedUpdate()
            try_jump = true;
        }
    }
    private void FixedUpdate()
    {
        Move(direction);
        if (try_jump)
        {
            Jump();
        }

    } 

    private void Move(Vector2 direction) 
    {
        rb.velocity = direction;
        // Create a velocity in a specific direction
    }

    private void Jump()
    {
        rb.velocity = new Vector2(0, jumpspeed);
        try_jump = false;
    } 

}