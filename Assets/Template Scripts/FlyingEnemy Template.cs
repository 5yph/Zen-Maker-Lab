using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyTemplate : MonoBehaviour
{
    [SerializeField] private float movespeed = 3f;
    [SerializeField] private bool invincible = false;
    private Vector2 direction;
    private bool to_point_2 = true; // are we going to point 2?

    public Transform point1; // first waypoint
    public Transform point2; // second waypoint
    private Rigidbody2D enemy;

    // Start is called before the first frame update
    void Start()
    {
        // SET the 'enemy' var to the correct Rigidbody, like we did for the basic enemy.
        // We also want to simulate flying, change the gravity of 'enemy' to best do this.
    }

    // Update is called once per frame
    void Update()
    {

        /* USE knowledge of 2D vector subtraction to set values for our direction vector.
           
         By using an if-else statement, test if we are going to point 2 with 'to_point_2'. If so,
         then we should set our direction vector to point from our current position to point2.
         If not, then we go to point 1, and set our direction vector to point to point1 instead.

         Hint: transform.position gives us the position of our enemy at any time.
         point1 and point2 are objects of the Transform class, and thus their position is point#.position.
         
         */

        if (Vector2.Distance(point2.position, transform.position) < 0.1f && to_point_2 == true)
        {
            // if we just arrived at point2 at a close enough distance
            to_point_2 = false;
        }
        else if (Vector2.Distance(point1.position, transform.position) < 0.1f && to_point_2 == false)
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
            collision.gameObject.GetComponent<Damage>().DealDamage(1);
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a line between the 2 waypoints
        Gizmos.color = Color.black;
        Gizmos.DrawLine(point1.position, point2.position);
    }

}
