using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
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
        if (speed < 0.5)
        {
            // check if our enemy not moving, flip direction
            // don't put speed at 0 as sometimes it may not register
            moves_right = !moves_right;
            // if we were moving right, we now move left. If we were moving left, we now move right
            Move();
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
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

        if (collision.gameObject.tag == "Pit")
        {
            // falling in a pit results in instant death
            Destroy(gameObject);
        }

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
