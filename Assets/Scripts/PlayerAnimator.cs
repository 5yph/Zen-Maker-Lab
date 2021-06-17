using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    PlayerMovement player;
    PlayerProjectileTrigger shooter;
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer sprite;

    public bool facing_right = true; // Sprite by default is facing right

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerMovement>();
        shooter = GetComponent<PlayerProjectileTrigger>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // flip character if move direction is opposite to before
        float direction = Input.GetAxisRaw("Horizontal");
        if (direction > 0 && !facing_right)
        {
            Flip();
        } else if (direction < 0 && facing_right)
        {
            Flip();
        }

        if (player.direction.magnitude < 0.1)
        {
            // not moving
            animator.SetBool("Moving", false);
        } else
        {
            animator.SetBool("Moving", true);
        }

        if (rb.velocity.y > 0.1)
        {
            // player is shooting up into the air
            animator.SetBool("Falling", false);
            animator.SetBool("Jumping", true);
        } else if (rb.velocity.y < -0.1)
        {
            // player is falling
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", true);
        } else
        {
            animator.SetBool("Falling", false);
            animator.SetBool("Jumping", false);
        }

        if (player.crouch)
        {
            // player is crouching 
            animator.SetBool("Crouching", true);
        }

        if (player.try_uncrouch && !player.ceiling_check)
        {
            // if player can successfully uncrouch
            animator.SetBool("Crouching", false);
        }

        if (shooter.shooting)
        {
            // if player is shooting
            animator.SetBool("Shooting", true);
        } else
        {
            animator.SetBool("Shooting", false);
        }

    }

    void Flip()
    {
        // Flip the parent object and all attached child objects

        Vector3 newScale = transform.localScale;
        newScale.x = newScale.x * -1;
        transform.localScale = newScale;

        facing_right = !facing_right;
    }
}
