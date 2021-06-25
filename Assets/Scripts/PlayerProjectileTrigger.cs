using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileTrigger : MonoBehaviour
{
    public GameObject projectile; // what projectile is this object shooting
    public Transform spawn; // where projectiles spawn (should be child object)

    PlayerAnimator animator;
    Projectile projectile_settings;
    Damage damage;
    
    [SerializeField] private float cooldown; // fire rate of projectile, in seconds
    private float next_time_can_fire; // when we can fire the next projectile
    [HideInInspector] public bool shooting = false; // are we shooting (for animator purposes)

    private void Start()
    {
        animator = GetComponent<PlayerAnimator>();
        projectile_settings = projectile.GetComponent<Projectile>();
        damage = GetComponent<Damage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && !damage.dead && !damage.respawning) // Shoot if holding button and not dead or respawning
        {
            // We need to account for the direction our player is facing
            // Since the projectile spawn point is flips with the player, we just 
            // need to change the direction of the resultant projectile

            if (animator.facing_right && projectile_settings.velocity.x < 0)
            {
                // if we are facing right but projectile velocity points left
                projectile_settings.velocity.x *= -1;
            } else if (!animator.facing_right && projectile_settings.velocity.x > 0)
            {
                // if we are facing left but projectile velocity points right
                projectile_settings.velocity.x *= -1;
            }

            shooting = true;
            if (next_time_can_fire <= Time.time)
            {
                Instantiate(projectile, spawn.position, spawn.rotation);
                next_time_can_fire = Time.time + cooldown; // next time we can fire is current time + cooldown
            }


        } else
        {   // not pressing shoot button
            shooting = false;
        }

    }
}
