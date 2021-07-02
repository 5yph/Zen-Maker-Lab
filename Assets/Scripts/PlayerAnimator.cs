using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    PlayerMovement player;
    PlayerProjectileTrigger shooter;
    Animator animator;
    Rigidbody2D rb;
    Damage damage;
    [SerializeField] AnimationClip death_animation;
    [SerializeField] AnimationClip respawn_animation;

    public bool facing_right = true; // Sprite by default is facing right
    [HideInInspector] public float death_time; // How long it takes to die (by animation standards)
    [HideInInspector] public float respawn_time; // How long it takes to respawn (by animation standards)

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerMovement>();
        shooter = GetComponent<PlayerProjectileTrigger>();
        damage = GetComponent<Damage>();

        death_time = death_animation.length / animator.GetFloat("DeathMultiplier");
        respawn_time = respawn_animation.length / animator.GetFloat("RespawnMultiplier");
    }

    // Update is called once per frame
    void Update()
    {
        // flip character if move direction is opposite to before
        float direction = Input.GetAxisRaw("Horizontal");
        if (direction > 0 && !facing_right && !damage.dead && !damage.respawning) // don't flip if we are dead
        {
            Flip();
        } else if (direction < 0 && facing_right && !damage.dead && !damage.respawning)
        {
            Flip();
        }

        if (player.direction.magnitude < 1)
        {
            // not moving
            animator.SetBool("Moving", false);
        } else
        {
            animator.SetBool("Moving", true);
        }

        if (rb.velocity.y > 5) // You may need to adjust these values to suit you best
        {
            // player is shooting up into the air
            animator.SetBool("Falling", false);
            animator.SetBool("Jumping", true);
        } else if (rb.velocity.y < -5)
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
        } else
        {
            animator.SetBool("Crouching", false);
        }

        /*
        if (player.try_uncrouch && !player.ceiling_check)
        {
            // if player can successfully uncrouch
            animator.SetBool("Crouching", false);
        } */

        if (shooter.shooting)
        {
            // if player is shooting
            animator.SetBool("Shooting", true);
        } else
        {
            animator.SetBool("Shooting", false);
        }

        if (damage.dead)
        {
            animator.SetBool("Dead", true); // Ensure this matches your animation name!
        } else
        {
            animator.SetBool("Dead", false);
        }

        if (damage.respawning)
        {
            animator.SetBool("Respawning", true);
        } else
        {
            animator.SetBool("Respawning", false);
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
