using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileTrigger : MonoBehaviour
{
    public GameObject projectile; // what projectile is this object shooting
    public Transform spawn; // where projectiles spawn (should be child object)
    
    [SerializeField] private float cooldown; // fire rate of projectile, in seconds
    private float next_time_can_fire; // when we can fire the next projectile
    public bool shooting = false; // are we shooting (for animator purposes)

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1")) // Get button allows you to hold the button
        {
            shooting = true;
            if (next_time_can_fire <= Time.time)
            {
                // if user presses the fire button
                Instantiate(projectile, spawn.position, spawn.rotation);
                next_time_can_fire = Time.time + cooldown; // next time we can fire is current time + cooldown
               // shooting = true;
            } // else
            // {
               // shooting = false;
           // }
        } else
        {
            shooting = false;
        }

    }
}
