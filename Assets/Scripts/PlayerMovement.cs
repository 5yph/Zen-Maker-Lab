using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D[] hitboxes;

    private CircleCollider2D crouch_hitbox; // hitbox for when we crouch
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

    [SerializeField] private float crouch_height_modifier; // a decimal value from 0.5-0.99 that tells you 
    // what percentage of original height the crouched height will be. DO NOT SET LOWER THAN 0.5
    [SerializeField] private Vector2 ceiling_check_offset; // where should our circle detecting ground be located?
    [SerializeField] private float ceiling_check_radius = 0.25f; // how big should our ground check circle be

    [HideInInspector] public Vector2 direction; // movement direction

    private int jump_count = 2; // how many times can we jump
    private bool grounded = false; // is the character touching ground?
    private bool try_jump = false; // is the player trying to jump?
    private bool jump_cancelled = false; // did player cancel jump?

    [HideInInspector] public bool crouch = false; // is the player crouching
    [HideInInspector] public bool try_uncrouch = false; // is the player trying to stand up
    [HideInInspector] public bool ceiling_check = false; // is there a ceiling above us?

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hitboxes = GetComponents<CapsuleCollider2D>(); // doesn't matter which capsule collider it retrieves first
        crouch_hitbox = GetComponent<CircleCollider2D>();
        crouch_hitbox.enabled = false; // disable circle collider intially
        // get reference to components, allows us to use built-in functions
    }

    void Update()
    {   // detect input in Update() so we don't miss any inputs
        float x_component = Input.GetAxisRaw("Horizontal");

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

        if (Input.GetKey(KeyCode.S) && grounded) // replace with key of choice
        {
            // user presses crouch
            crouch = true;
            try_uncrouch = false;
        } 
        
        if ((Input.GetKeyUp(KeyCode.S) || !grounded) && crouch)
        {
            // user stops crouching if stops holding crouch or not on ground anymore
            try_uncrouch = true;
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
                UnCrouch();
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
        return Physics2D.OverlapCircle((Vector2)transform.position + ceiling_check_offset, ceiling_check_radius, ceilingLayer);
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

        crouch_hitbox.enabled = true;

        for (int i = 0; i < hitboxes.Length; i++)
        {
            hitboxes[i].enabled = false; // disable each collider
        }
    }

    private void UnCrouch()
    {
        // no ceiling above us, safe to stand up
        crouch_hitbox.enabled = false;
        for (int i = 0; i < 2; i++)
        {
                hitboxes[i].enabled = true;
        }

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