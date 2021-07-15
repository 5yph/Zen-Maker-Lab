using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attach this script to the player object
public class GroundSlam : MonoBehaviour
{
    PlayerMovementDash player; // CHANGE TO 'PlayerMovement'
    Rigidbody2D rb;
    private RaycastHit2D hit; // what we hit with our raycast
    Vector2 offset_vec;
    public LayerMask enemyLayer; // what is our enemies

    [SerializeField] private float horizontal_offset = 5f; // where the raycast starts x-wise
    [SerializeField] private float vertical_offset = 5f; // where the raycast start y-wise
    [SerializeField] private float range = 10f; // how far down the raycast points

    [SerializeField] private float slamspeed; // how fast he slam down
    [HideInInspector] public bool slamming = false; // are we slamming into the ground?


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerMovementDash>(); // CHANGE TO 'PlayerMovement'
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        offset_vec = new Vector2(transform.position.x + horizontal_offset, transform.position.y + vertical_offset);
        hit = Physics2D.Raycast(offset_vec, Vector2.right, range, enemyLayer);

        if (Input.GetKeyDown(KeyCode.LeftControl) && !player.grounded)
        {
            // By default, groundslam key is left control
            slamming = true;
            rb.velocity = new Vector2(rb.velocity.x, -1 * slamspeed);
            
            if (player.is_dashing || player.is_dashing_up)
            {
                // groundslam cancels dash
                player.StopCoroutine("DashWait");
                player.is_dashing = false;
                player.is_dashing_up = false;
                player.allow_flip = true;
                player.normal_dash_cooldown = false;
            }
        }

        if (slamming && player.grounded)
        {
            if (hit.collider != null)
            {
                // our raycast hits enemy
                Destroy(hit.collider.gameObject);
                // This kills all enemies on our layer, no matter if "invincible" is checked
                // on their scripts
            }
            slamming = false;
        }
    }
    private void OnDrawGizmos()
    {
        // Draw raycast line for debugging
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector2(transform.position.x + horizontal_offset, transform.position.y + vertical_offset), Vector2.right * range);
    }

}
