using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int lives = 1;
    private int current_life;

    public Transform Spawn; // where we respawn
    private Rigidbody2D rb;

    [HideInInspector] public bool dead = false; // are we dead?
    [HideInInspector] public bool respawning = false; // are we respawning?
    private float death_time; // how long it takes for us to die after hitting
    private float respawn_time; // how long it takes for us to respawn after dying

    private void Start()
    {
        current_life = lives;
        rb = GetComponent<Rigidbody2D>();
        // get the death time from the animation script
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spikes")
        {
            DealDamage(1); 
        }

        // We will code enemy damage in their respective scripts
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // triggers are colliders that are passable, but can still be detected for collisions
        if (collision.gameObject.tag == "Checkpoint")
        {
            Spawn = collision.transform; // make the new spawn our checkpoint position
        }

        if (collision.gameObject.tag == "Pit")
        {
            // falling in a pit results in instant death
            Die();
        }
    }


    public void DealDamage(int damage) // make this public so anything can deal damage to player
    {
        current_life = current_life - damage;
        if (current_life <= 0)
        {
            Die();
        }
    }

    public void Die() // public so anything can cause instant death
    {
        dead = true;
        // gets the length of time that our death animation will play

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
        current_life = lives; // reset lives

        respawn_time = GetComponent<PlayerAnimator>().respawn_time;
        yield return new WaitForSeconds(respawn_time);

        respawning = false;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        // Go back to default constraints (freezing rotation).
        rb.velocity = Vector2.down; // add some gravity so it doesn't freeze in air
    }

}