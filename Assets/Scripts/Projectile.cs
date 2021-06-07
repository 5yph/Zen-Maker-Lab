using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
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

    private void Start()
    {
        if (!gravity)
        {
            projectile.gravityScale = 0; // turn gravity off if no arc wanted
        }

        Destroy(gameObject, lifespan);
        // kill the projectile after some time
    }

    void Update()
    {
        // if projectile collides with enemy...
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
