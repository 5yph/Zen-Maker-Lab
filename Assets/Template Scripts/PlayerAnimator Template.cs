using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorTemplate : MonoBehaviour
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
        if (player.direction.magnitude < 1)
        {
            // not moving
            animator.SetBool("Moving", false);
        }
        else
        {
            animator.SetBool("Moving", true);
        }

        // TASK #1

        /* Use the Rigidbody2D (rb) component to do the following:
           
           Check if our player is jumping (or is just getting launched into the air).
           Check if the player is falling.
           Check if neither is the case.
           (Hint: rb.velocity.y gives you info about the velocity in the y direction)
        
           We want to keep track of two booleans: "Jumping" and "Falling", 
           in the same manner as the above if-else statement. It is up to you to figure
           out in which cases these booleans are set to true and false.
         */

        // TASK #2

        // Check if the player is crouching, if so set the "Crouch" boolean appropriately.
        // Hint: Something in our 'player' object gives us info about if our player crouches.

        // TASK #3

        // Check if our player is shooting (optional). If so, set the "Shooting" boolean.
        // Hint: Something in our 'shooter' objects gives us info about if our player is shooting.

        // TASK #4

        // Check if our player is dead. If so, set the "Dead" boolean.
        // Hint: Something in our 'damage' objects gives us info about if our player is dead.

        // TASK #5

        // Check if our player is respawning. If so, set the "Respawning" boolean.
        // Hint: Something in our 'damage' objects gives us info about if our player is respawning.
    }
}
