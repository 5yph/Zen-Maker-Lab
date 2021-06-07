using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileTriggerTemplate : MonoBehaviour
{
    public GameObject projectile; // what projectile is this object shooting
    public Transform spawn; // where projectiles spawn (should be child object)

    [SerializeField] private float cooldown; // fire rate of projectile, in seconds
    private float next_time_can_fire; // when we can fire the next projectile

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (next_time_can_fire <= Time.time)
            {
                /* Here we are only letting our player shoot if the variable next_time_can_fire
                is less than the amount of time the game has been running for (Time.time). We need to do 
                2 things:

                1. Instantiate a projectile at our projectile spawn point using Instantiate()
                2. Adjust the variable next_time_can_fire so that the next time we can shoot only occurs
                after our cooldown has been passed.
                */

            }
        }
        
    }
}
