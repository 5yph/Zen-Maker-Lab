using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTemplate : MonoBehaviour
{
    [SerializeField] private float lifespan = 3f; // how long the projectile is alive (in seconds)
    [SerializeField] private bool gravity = false; // projectile uses gravity
    [SerializeField] private bool turn_off_ray = false;

    public Vector2 velocity; // 2D vector representing direction of projectile 

    private Rigidbody2D projectile;

    // Awake is called once script is loaded
    void Awake()
    {
        projectile = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        /* We need to do 2 things once the game starts.
         
        1. Check if we enable gravity by the 'gravity' boolean. If we didn't, then we disable gravity
        on our projectiles rigid body by modifying projectile.gravityScale. 
         
        2. We need to destroy our projectile after some time using Destroy(). It takes 2 parameters: the
        object to be destroyed and the time (lifespan) until it gets destroyed.
         
         */
    }

    private void FixedUpdate()
    {
        transform.position = transform.position + new Vector3(velocity.x * Time.fixedDeltaTime, velocity.y * Time.fixedDeltaTime, 0);
    }

    private void OnDrawGizmos()
    {
        if (turn_off_ray)
        {
            return;
        }

        // Draws the vector line of where the projectile will head
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, new Vector3(velocity.x, velocity.y, 0));
    }
}
