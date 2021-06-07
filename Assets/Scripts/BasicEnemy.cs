using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    // Script for enemy that just walks forward

    [SerializeField] private float movespeed = 5f;
    [SerializeField] private bool moves_right = false; // does the enemy move right? (default is left)

    private Rigidbody2D enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (moves_right)
        {
            enemy.velocity = new Vector2(movespeed, enemy.velocity.y);
        } else
        {
            enemy.velocity = new Vector2(-movespeed, enemy.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FriendlyProjectile")
        {
            Destroy(gameObject);
            // if enemy gets hit with player projectile, it should die.
            Destroy(collision.gameObject); 
            // the projectile is also destroyed
        }

        if (collision.gameObject.tag == "Player")
        {
            // if enemy hits a player, we deal damage to them
            collision.gameObject.GetComponent<Damage>().DealDamage(1);
        }

    }

}
