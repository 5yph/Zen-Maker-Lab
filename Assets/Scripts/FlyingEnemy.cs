using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] private float movespeed = 3f;
    [SerializeField] private bool invincible = false;
    private Vector2 direction;
    private bool to_point_2 = true; // are we going to point 2

    public Transform point1; // first waypoint
    public Transform point2; // second waypoint
    private Rigidbody2D enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
        enemy.gravityScale = 0; // no gravity for flying enemies
    }

    // Update is called once per frame
    void Update()
    {
        if (to_point_2)
        {
            direction = point2.position - transform.position;
            // we are going to point 2
        } else
        {
            direction = point1.position - transform.position;
            // we are going to point 1
        }

        // change buffer depending on size of enemy and area
        if (Vector2.Distance(point2.position, transform.position) < 1f && to_point_2 == true)
        {
            // if we just arrived at point2 at a close enough distance
            to_point_2 = false;
        } else if (Vector2.Distance(point1.position, transform.position) < 1f && to_point_2 == false)
        {
            // if we just arrived at point1
            to_point_2 = true;
        }

    }

    private void FixedUpdate()
    {
        direction.Normalize(); // only care about the direction
        enemy.velocity = direction * movespeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // same as basic enemy, but we exclude pit

        if (collision.gameObject.tag == "FriendlyProjectile")
        {
            if (!invincible)
            {
                Destroy(gameObject);
                // if enemy gets hit with player projectile, it should die.
            }

            Destroy(collision.gameObject);
            // the projectile is also destroyed
        }

        if (collision.gameObject.tag == "Player")
        {
            // if enemy hits a player, we deal damage to them
            collision.gameObject.GetComponent<Damage>().Die();
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a line between the 2 waypoints
        Gizmos.color = Color.black;
        Gizmos.DrawLine(point1.position, point2.position);
    }

}
