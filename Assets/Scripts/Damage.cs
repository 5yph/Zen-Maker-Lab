using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float life = 1;
    public Transform Spawn; // where we respawn

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spikes")
        {
            DealDamage(1); 
        }

        if (collision.gameObject.tag == "Enemy")
        {
            DealDamage(1);
        }

    }

    public void DealDamage(int damage) // make this public so anything can deal damage to player
    {
        life = life - damage;
        if (life <= 0)
        {
            Die();
        }
    }

    public void Die() // public so anything can cause instant death (like falling in hole)
    {
        // Respawn
        transform.position = Spawn.position;
        // move our character back to spawn position
    }

}