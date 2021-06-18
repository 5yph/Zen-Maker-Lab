using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float life = 1;
    public Transform Spawn; // where we respawn
    [HideInInspector] public bool dead = false; // are we dead?

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spikes")
        {
            DealDamage(1); 
        }

        if (collision.gameObject.tag == "Pit")
        {
            // falling in a pit results in instant death
            Die();
        }

        // We will code enemy damage in their respective scripts
    }

    public void DealDamage(int damage) // make this public so anything can deal damage to player
    {
        life = life - damage;
        if (life <= 0)
        {
            dead = true;
            Die();
        }
    }

    public void Die() // public so anything can cause instant death
    {
        // Respawn
        transform.position = Spawn.position;
        // move our character back to spawn position
        dead = false; // respawned
    }

}