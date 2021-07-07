using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Damage : MonoBehaviour
{   // If player gets hit by enemy, he will insta-die

    public Transform Spawn; // where we respawn
    private Rigidbody2D rb;
    private PlayerMovement player;

    [HideInInspector] public bool dead = false; // are we dead?
    [HideInInspector] public bool respawning = false; // are we respawning?
    private float death_time; // how long it takes for us to die after hitting
    private float respawn_time; // how long it takes for us to respawn after dying

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // get the death time from the animation script
        player = GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spikes")
        {
            Die(); 
        }

        // We will code enemy damage in their respective scripts
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // triggers are colliders that are passable, but can still be detected for collisions

        if (collision.gameObject.tag == "Pit")
        {
            // falling in a pit results in instant death
            Die();
        }
    }

    public void Die() // public so anything can cause instant death
    {
        dead = true;
        // gets the length of time that our death animation will play
        player.allow_flip = false;
        
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        // Freeze all movement of player when we die

        StartCoroutine("Respawn");

        // move our character back to spawn position
    }

    IEnumerator Respawn()
    {
        death_time = GetComponent<PlayerAnimator>().death_time;
        yield return new WaitForSeconds(death_time);
        // spend some time before respawning (to play death animation)

        dead = false;
        respawning = true;
        transform.position = Spawn.position; // Go to respawn point

        respawn_time = GetComponent<PlayerAnimator>().respawn_time;
        yield return new WaitForSeconds(respawn_time);

        respawning = false;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        // Go back to default constraints (freezing rotation).
        player.allow_flip = true;
        rb.velocity = Vector2.down; // add some gravity so it doesn't freeze in air
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(Spawn.position, 1f);
    }

}