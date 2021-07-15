using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// RENAME THIS CLASS AND SCRIPT TO 'PLAYERMOVEMENT'
public class PlayerMovementDash : MonoBehaviour
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

    [Space]

    [Header("Dash")]
    [SerializeField] private float dash_multiplier; // how much faster is dash than movement
    [SerializeField] private float jump_dash_multiplier; // how much faster is the jump dash than jumpspeed
    [SerializeField] private float dash_time; // how long the dash lasts (seconds)
    [SerializeField] private float dash_cooldown; // how long until we can dash again?

    [HideInInspector] public Vector2 direction; // movement direction

    private int jump_count = 2; // how many times can we jump
    [HideInInspector] public bool grounded = false; // is the character touching ground?
    private bool try_jump = false; // is the player trying to jump?
    private bool jump_cancelled = false; // did player cancel jump?

    [HideInInspector] public bool crouch = false; // is the player crouching
    [HideInInspector] public bool is_dashing = false; // is the player dashing
    [HideInInspector] public bool is_dashing_up = false; // is the player dashing upwards

    [HideInInspector] public bool normal_dash_cooldown = false; // is our regular dash on cooldown
    private bool air_dash_cooldown = false; // is our air dash cooling down
    
    private bool try_uncrouch = false; // is the player trying to stand up
    private bool ceiling_check = false; // is there a ceiling above us?

    [Space]

    public bool facing_right = true; // is our player facing right? public so projectile script can access
    public bool allow_flip = true; // turn false is player is dead

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
            direction = new Vector2(x_component * crouchspeed, rb.velocity.y);
        }

        grounded = IsGrounded();
        ceiling_check = UnderCeiling();

        if (grounded)
        {
            jump_count = 2;
            air_dash_cooldown = false; // can re-dash up
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

        if ((Input.GetKeyDown(KeyCode.LeftShift)) && Input.GetButton("Jump") && !is_dashing && !is_dashing_up && !crouch)
        {
            // if we press dash while holding jump (dash upwards)
            if (!air_dash_cooldown)
            {
                is_dashing_up = true;
                air_dash_cooldown = true;
                StartCoroutine("DashWait");
            }
        } else if ((Input.GetKeyDown(KeyCode.LeftShift)) && !is_dashing && !crouch)
        {            
            // if we press dash button
            if (grounded)
            {
                // if we are on the ground, we can dash as long as cooldown allows 
                if (!normal_dash_cooldown)
                {
                    // normal dash not on cooldown
                    is_dashing = true;
                    normal_dash_cooldown = true;

                    StartCoroutine("DashWait");
                }
            } else if (!air_dash_cooldown)
            {
                // if we are in the air but can still dash in the air
                is_dashing = true;
                air_dash_cooldown = true;

                StartCoroutine("DashWait");
            }
            
        }

        if (x_component > 0 && !facing_right) // don't flip if we are dead
        {
            if (allow_flip)
            {
                Flip();
            }
        }
        else if (x_component < 0 && facing_right)
        {
            if (allow_flip)
            {
                Flip();
            }
        }

    }
    private void FixedUpdate()
    {
        if (!is_dashing && !is_dashing_up)
        {
            Move(direction);
        }

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

        if (is_dashing || is_dashing_up)
        {
            Dash(direction);
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

    private void Dash(Vector2 direction)
    {
        allow_flip = false;

        if (is_dashing_up)
        {
            // player is dashing upwards
            if (!ceiling_check)
            {
                // can't be under ceiling, otherwise will stick to roof
                direction.y = short_jumpspeed * jump_dash_multiplier;
                rb.velocity = direction;
            } else
            {
                // stop dash immediately if we hit ceiling
                is_dashing_up = false;
            }
        } else if (Mathf.Abs(direction.x) < movespeed)
        {
            // if player is not moving horizontally and just dashes
            if (facing_right)
            {
                rb.velocity = new Vector2(movespeed * dash_multiplier, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-movespeed * dash_multiplier, rb.velocity.y);
            }
        } else
        {
            // player is dashing horizontally or diagonally
            if (facing_right)
            {
                direction.x = movespeed * dash_multiplier;
            }
            else
            {
                direction.x = -1 * movespeed * dash_multiplier;
            }
            rb.velocity = direction;
        }
    }

    IEnumerator DashWait()
    {
        yield return new WaitForSeconds(dash_time);
        is_dashing = false;
        is_dashing_up = false;
        allow_flip = true;
        yield return new WaitForSeconds(dash_cooldown);
        normal_dash_cooldown = false;
    }

    void Flip()
    {
        // Flip the parent object and all attached child objects

        Vector3 newScale = transform.localScale;
        newScale.x = newScale.x * -1;
        transform.localScale = newScale;

        facing_right = !facing_right;
    }

    private void OnDrawGizmos()
    {
        // Draw the circle that detects where ground + ceiling is
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + grounded_offset, grounded_radius);
        Gizmos.DrawWireSphere((Vector2)transform.position + ceiling_check_offset, ceiling_check_radius);

    }
}