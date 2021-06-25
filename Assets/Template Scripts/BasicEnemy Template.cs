using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyTemplate : MonoBehaviour
{
    // Script for enemy that just walks forward

    [SerializeField] private float movespeed = 5f;
    [SerializeField] private bool moves_right = false; // does the enemy move right? (default is left)
    [SerializeField] private bool invincible = false; // can enemy die by player?
    private float speed; // used to check if enemy is moving

    private Rigidbody2D enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        speed = enemy.velocity.magnitude;
        // Get the current speed of our enemy

        /* CODE an if statement that checks if our speed is sufficiently small (less than 0.1).
         If it is, that means our enemy has stopped, likely because they crashed into a wall.
         Then, we need to change the direction of our enemy, and finally call Move() so we can
         update which way it goes.

         Hint: look carefully at how Move() determines which direction the enemy goes.
        */
    }

    void FixedUpdate()
    {
        Move(); // our enemy moves every time physics is updated
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /* This function executes whenever the enemy collides with something.
        The object that collides with our enemy is stored as the "collision" variable.
        
        Hint: We can use 'collision.gameObject' to get the object that collided with the enemy.
        Thus we can also use 'collision.gameObject.tag' to check the tag of the object that 
        collided with the enemy.
        Look below for an example.
        */

        if (collision.gameObject.tag == "Player")
        {
            // if enemy hits a player, we deal damage to them
            collision.gameObject.GetComponent<Damage>().Die();
            // Here, we get the 'Damage' script from the collided object, which is the player.
            // Then, from that damage script, we can execute the public function Die
        }

        /* CODE 2 more if statements here.
         
         1. Check if the collided object's tag is "Pit". If our enemy hits a pit and falls into it,
         we should destroy our enemy using Destroy().

         2. Check if the collided object's tag is "FriendlyProjectile". If it is, and if our enemy is not invincible,
         then we should destroy both our enemy and the projectile. If our enemy is invincible, just destroy the
         projectile.
        
         Hint: Don't do Destroy(enemy). Since enemy is our rigidbody, this will just destroy their rigidbody
         and break physics. Remember the variable 'gameObject' always exists and refers to the object this script is 
         attached to. 
         */
    }


    private void Move()
    {
        if (moves_right)
        {
            enemy.velocity = new Vector2(movespeed, enemy.velocity.y);
        }
        else
        {
            enemy.velocity = new Vector2(-movespeed, enemy.velocity.y);
        }
    }
}

