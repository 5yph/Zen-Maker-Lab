using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D hitbox;
    public LayerMask groundLayer;
    
    [Header("Speeds")]
    [SerializeField] private float movespeed;
    [SerializeField] private float jumpspeed;
    [SerializeField] private float short_jumpspeed; // jump speed if jump button only tapped

    [Space]

    [Header("Ground Check")]
    [SerializeField] private Vector2 grounded_offset; // where should our circle detecting ground be located?
    [SerializeField] private float grounded_radius = 0.25f; // how big should our ground check circle be

    private Vector2 direction; // movement direction
    private int jump_count = 2; // how many times can we jump
    private bool grounded = false; // is the character touching ground?
    private bool try_jump = false; // is the player trying to jump?
    private bool jump_cancelled = false; // did player cancel jump?

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
        grounded = IsGrounded();

        if (grounded) {
            jump_count = 2;
        }

        if (Input.GetButtonDown("Jump")) {   
            // user presses jump
            if (grounded) {
                try_jump = true;
                jump_count--;
            } else if (!grounded && (jump_count == 2)) {
                // we are in the air, but we still have 2 jumps
                // i.e. we fell off a cliff
                try_jump = true;
                jump_count = 0; // only jump once
            } else if (jump_count > 0) {
                // Player in air, but can still jump 
                try_jump = true;
                jump_count--;
            }
        }
        
        if (Input.GetButtonUp("Jump") && !grounded) {
            // if the player cancelled the jump in mid-air, don't jump as high
            jump_cancelled = true;
        }
    }
    private void FixedUpdate()
    {
        Move(direction);
        if (try_jump) {
            Jump();
        }
        
        if (jump_cancelled) {
            if (rb.velocity.y > short_jumpspeed) {
                // change characters velocity to short jump velocity
                rb.velocity = new Vector2(rb.velocity.x, short_jumpspeed);
            }
            jump_cancelled = false;
        }               
    } 

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle((Vector2)transform.position + grounded_offset, grounded_radius, groundLayer);
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
        // Draw the circle that detects where ground is
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + grounded_offset, grounded_radius);
    }
}