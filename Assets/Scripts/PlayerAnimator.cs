using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    PlayerMovement player;
    PlayerProjectileTrigger shooter;
    Animator animator;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerMovement>();
        shooter = GetComponent<PlayerProjectileTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.direction.magnitude < 0.1)
        {
            // not moving
            animator.SetBool("Moving", false);
        } else
        {
            animator.SetBool("Moving", true);
        }

        if (rb.velocity.y > 0.001)
        {
            // player is shooting up into the air
            animator.SetBool("Falling", false);
            animator.SetBool("Jumping", true);
        } else if (rb.velocity.y < -0.001)
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
}
