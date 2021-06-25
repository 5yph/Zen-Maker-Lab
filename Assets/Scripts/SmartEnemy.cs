using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartEnemy : MonoBehaviour
{
    [SerializeField] private float movespeed = 3f;
    [SerializeField] private bool moves_right = false; 
    [SerializeField] private bool invincible = false;
    [SerializeField] private float offset = 3f; // where the raycast originates
    [SerializeField] private float distance = 2f; // how far down the raycast points
   
    private float speed; // used to check if enemy is moving

    private RaycastHit2D hit; // what we hit with our raycast
    private Vector2 offset_vec; // vector representing where our offset is
    private Rigidbody2D enemy;
    public LayerMask groundLayer; // what is ground


    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (moves_right && offset < 0)
        {
            // if we are moving right, ensure offset is to the right of us
            offset = -offset;
        } else if (!moves_right && offset > 0)
        {
            // if we are moving left, ensure offset is to the left of us
            offset = -offset;
        }

        speed = enemy.velocity.magnitude;
        if (speed < 0.1)
        {
            // check if our enemy not moving, flip direction
            // don't put speed at 0 as sometimes it may not register
            moves_right = !moves_right;
            // if we were moving right, we now move left. If we were moving left, we now move right
            Move();
        }

        offset_vec = new Vector2(transform.position.x + offset, transform.position.y);
        hit = Physics2D.Raycast(offset_vec, -Vector2.up, distance, groundLayer);
        // cast raycast straight down, in front of our enemy by a bit.
        if (hit.collider == null)
        {
            // our raycast is not hitting ground (due to layermask)
            moves_right = !moves_right; // change direction
        }
        
    }
    void FixedUpdate()
    {
        Move();
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

    private void OnDrawGizmos()
    {
        // Draw raycast line for debugging
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector2(transform.position.x + offset, transform.position.y), -Vector2.up * distance);
    }

}
