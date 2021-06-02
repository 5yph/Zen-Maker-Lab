using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileTrigger : MonoBehaviour
{
    public GameObject projectile; // what projectile is this object shooting
    public Transform spawn; // where projectiles spawn (should be child object)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // if user presses the fire button
            Instantiate(projectile, spawn.position, spawn.rotation);
            // Destroy(projectile, 5f);
        }

    }
}
