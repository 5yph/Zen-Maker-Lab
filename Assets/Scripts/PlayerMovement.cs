using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D hitbox;
    public LayerMask groundLayer; // what is considered ground
    public LayerMask ceilingLayer; // what is considered ceiling
    
    [Header("Speeds")]
    [SerializeField] private float movespeed;
    [SerializeField] private float jumpspeed;
    [SerializeField] private float short_jumpspeed; // jump speed if jump button only tapped
    [SerializeField] private float crouchspeed; // speed of character when crouching

    [Space]

    [Header("Ground Check")]
    [SerializeField] private Vector2 grounded_offset; // where should our circle detecting ground be located?
    [SerializeField] private float grounded_radius = 0.25f; // how big should our ground check circle be

    [Space]

    [Header("Crouch")]

    [SerializeField] private float crouch_height_modifier; // a decimal value from 0.1-0.9 that tells you 
    // that the character crouches to 50% of their height, and thus only half of their capsule collider should exist.
    [SerializeField] private Vector2 ceiling_check_offset; // where should our circle detecting ground be located?
    [SerializeField] private float ceiling_check_radius = 0.25f; // how big should our ground check circle be

    private Vector2 direction; // movement direction
    private Vector2 hitbox_size_original; // width and height of capsule collider
    private Vector2 hitbox_offset_original; // original hitbox offset

    private int jump_count = 2; // how many times can we jump
    private bool grounded = false; // is the character touching ground?
    private bool try_jump = false; // is the player trying to jump?
    private bool jump_cancelled = false; // did player cancel jump?

    private bool crouch = false; // is the player crouching
    private bool try_uncrouch = false; // is the player trying to stand up
    private bool ceiling_check = false; // is there a ceiling above us?

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<CapsuleCollider2D>();
        hitbox_size_original = hitbox.size;
        hitbox_offset_original = hitbox.offset;
        // get reference to components, allows us to use built-in functions
    }

    void Update()
    {   // detect input in Update() so we don't miss any inputs

        float x_component = Input.GetAxisRaw("Horizontal");
        float y_component = Input.GetAxisRaw("Vertical");

        if (!crouch)
        {
            // uncrouched movespeed
            direction = new Vector2(x_component * movespeed, rb.velocity.y);
        } else
        {
            direction = new Vector2(x_component * crouchspeed, rb.velocity.y);
        }

        grounded = IsGrounded();
        ceiling_check = UnderCeiling();

        if (grounded)
        {
            jump_count = 2;
        }

        if (Input.GetButtonDown("Jump"))
        {   
            // user presses jump
            if (grounded)
            {
                try_jump = true;
                jump_count--;
            } else if (!grounded && (jump_count == 2))
            {
                // we are in the air, but we still have 2 jumps
                // i.e. we fell off a cliff
                try_jump = true;
                jump_count = 0; // only jump once
            } else if (jump_count > 0)
            {
                // Player in air, but can still jump 
                try_jump = true;
                jump_count--;
            }
        }
        
        if (Input.GetButtonUp("Jump") && !grounded)
        {
            // if the player cancelled the jump in mid-air, don't jump as high
            jump_cancelled = true;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && grounded) // replace with key of choice
        {
            // user presses crouch
            crouch = true;
            try_uncrouch = false;
        } else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            try_uncrouch = true;
            crouch = false;
            // user stops holding crouch
        }

    }
    private void FixedUpdate()
    {
        Move(direction);

        if (try_jump)
        {
            Jump();
        }
        
        if (jump_cancelled)
        {
            JumpCancel();
        }               

        if (crouch)
        {
            Crouch();
        }

        if (try_uncrouch)
        {
            if (!ceiling_check)
            {
                unCrouch();
                // if there is a ceiling, we keep trying to uncrouch until
                // we find there isn't a ceiling -- unless the user presses crouch again
            }
        }
    } 

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle((Vector2)transform.position + grounded_offset, grounded_radius, groundLayer);
    }

    private bool UnderCeiling()
    {
        return Physics2D.OverlapCircle((Vector2)transform.position + ceiling_check_offset, ceiling_check_radius, ceilingLayer); ;
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

    private void JumpCancel()
    {
        if (rb.velocity.y > short_jumpspeed)
        {
            // change characters velocity to short jump velocity
            rb.velocity = new Vector2(rb.velocity.x, short_jumpspeed);
        }
        jump_cancelled = false;
    }


    private void Crouch()
    {
        hitbox.size = new Vector2(hitbox_size_original.x, hitbox_size_original.y / 2);
        // when crouching, halve the height of hitbox, but don't change width
        // we also need to move this hitbox to the base of the capsule
        hitbox.offset = new Vector2(hitbox_offset_original.x, -(hitbox_size_original.y) / 4);
    }

    private void unCrouch()
    {
        // no ceiling above us, safe to stand up
        hitbox.size = hitbox_size_original;
        hitbox.offset = hitbox_offset_original;
        // go back to original hitbox size and offset
        crouch = false;
        try_uncrouch = false;
    }

    private void OnDrawGizmos()
    {
        // Draw the circle that detects where ground + ceiling is
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + grounded_offset, grounded_radius);
        Gizmos.DrawWireSphere((Vector2)transform.position + ceiling_check_offset, ceiling_check_radius);

    }
}