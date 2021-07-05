using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// THIS IS THE TEMPLATE FOR THE THIRD VERSION OF THE PLAYER MOVEMENT SCRIPT
// Do not attempt unless you have already completed the previous templates
public class PlayerMovement3Template : MonoBehaviour
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
    [SerializeField] private Vector2 ceiling_check_offset; // where should our circle detecting ground be located?
    [SerializeField] private float ceiling_check_radius = 0.25f; // how big should our ground check circle be

    [HideInInspector] public Vector2 direction; // movement direction 

    private int jump_count = 2; // how many times can we jump
    private bool grounded = false; // is the character touching ground?
    private bool try_jump = false; // is the player trying to jump?
    private bool jump_cancelled = false; // did player cancel jump?

    [HideInInspector]  public bool crouch = false; // is the player crouching
    [HideInInspector]  public bool try_uncrouch = false; // is the player trying to stand up
    [HideInInspector]  public bool ceiling_check = false; // is there a ceiling above us?

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
        }
        else
        {
            // TASK #1

            /* IMPLEMENT a new way to deal with movement speed while crouched.
        Before we had a constant movespeed. Now, after checking that the player is crouched,
        our new movespeed should be the crouchspeed instead. 
        */ 
        }

        grounded = IsGrounded();
        
        // TASK #2
        // ADD A CEILING CHECK, just like the ground check, with the UnderCeiling() function

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
            }
            else if (!grounded && (jump_count == 2))
            {
                // we are in the air, but we still have 2 jumps
                // i.e. we fell off a cliff
                try_jump = true;
                jump_count = 0; // only jump once
            }
            else if (jump_count > 0)
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

            // TASK #3
            // We need to check if the user is pressing crouch, look into Input.GetKey() in documentation
            // Check if the user is pressing the "crouch" key (can be any key you wish).

            // If user presses crouch and he is grounded, we should change the values of vars 'crouch' and 'try_uncrouch'.
            // Implement this. ('crouch' calls the Crouch() function, 'try_uncrouch' calls UnCrouch() if no ceiling).


            // TASK #4
            // Create an if statement that allows us to try to uncrouch given the correct conditions
            // This should only happen if the user is already crouched
            
            // We try to uncrouch if: We release crouch OR we are no longer grounded.

            // When we try to uncrouch, the variable 'try_uncrouch' should change.
            // We do not modify crouch because maybe we aren't able to uncrouch yet (under ceiling)

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
            // TASK #5

            /* Before we can allow our character to uncrouch, we need to make sure that
            there is no overhead ceiling, otherwise we could get stuck.

            IMPLEMENT this. Remember UnCrouch() and ceiling_check.
            */
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