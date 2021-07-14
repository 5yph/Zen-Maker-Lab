using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    // Script for enemy that just walks forward

    [SerializeField] private float movespeed = 5f;
    private bool moves_right = false; // does the enemy move right? (default is left)
    [SerializeField] private bool facing_right = false; // is the enemy facing right?
    [SerializeField] private bool invincible = false; // can enemy die by player?
    [SerializeField] private float detect_wall;
    private float speed; // used to check if enemy is moving

    private Rigidbody2D enemy;
    private RaycastHit2D hit; // what we hit with our raycast
    private Vector2 ray_dir;
    public LayerMask collideLayer; // what layers our enemy will turn around after colldiing with

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        hit = Physics2D.Raycast(transform.position, ray_dir, detect_wall, collideLayer);

        if (hit.collider != null)
        {
            Flip();
        }

        if (facing_right)
        {
            ray_dir = new Vector2(1, 0);
            moves_right = true;
        }
        else
        {
            ray_dir = new Vector2(-1, 0);
            moves_right = false;
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
            collision.gameObject.GetComponent<Damage>().Die();
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

    void Flip()
    {
        // Flip the parent object and all attached child objects

        Vector3 newScale = transform.localScale;
        newScale.x = newScale.x * -1;
        transform.localScale = newScale;

        facing_right = !facing_right;
    }

    private void OnDrawGizmos()
    {
        if (facing_right)
        {
            Gizmos.DrawRay((Vector2)transform.position, Vector2.right * detect_wall);
        }
        else
        {
            Gizmos.DrawRay((Vector2)transform.position, Vector2.left * detect_wall);
        }
    }

}
