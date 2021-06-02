using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectile_speed = 20f;
    [SerializeField] private float lifespan = 3f; // how long the projectile is alive (in seconds)
    [SerializeField] private bool shoots_left = false; // projectile travels left (default is right)
    [SerializeField] private bool arc = false; // projectile arcs instead of shooting straight

    private Rigidbody2D projectile;

    // Awake is called once script is loaded
    void Awake()
    {
        projectile = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (!arc)
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
        transform.position = transform.position + new Vector3(projectile_speed * Time.fixedDeltaTime, 0, 0);

    }

}
