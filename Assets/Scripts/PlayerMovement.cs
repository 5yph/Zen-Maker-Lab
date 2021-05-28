using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D hitbox;
    public LayerMask groundLayer;
    
    [SerializeField] private float movespeed;
    [SerializeField] private float jumpspeed;
    [SerializeField] private Vector2 grounded_offset; // where should our circle detecting ground be located?

    private Vector2 direction; // movement direction
    private RaycastHit2D below_char; // represents what object is below us (if any)
    private bool can_double_jump = false;
    private bool grounded = false; // is the character touching ground?
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
        grounded = isGrounded();

        if (Input.GetButtonDown("Jump") && grounded) {
            // if we press the jump button, remember that the player wants to jump
            // we need to save this as a variable so it gets processed in FixedUpdate()
            try_jump = true;
            can_double_jump = true;
        } else if (Input.GetButtonDown("Jump") && can_double_jump) {
            // Player in air, but can still double jump 
            try_jump = true; 
            can_double_jump = false;
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

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle((Vector2)transform.position + grounded_offset, 0.25f, groundLayer);
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

    private void OnDrawGizmos()
    {
        // Draw the circle detecting where ground is

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + grounded_offset, 0.25f);
    }

}