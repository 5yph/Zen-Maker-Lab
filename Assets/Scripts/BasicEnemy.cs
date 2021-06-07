using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    // Script for enemy that just walks forward

    [SerializeField] private float movespeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FriendlyProjectile")
        {
            Destroy(gameObject);
            // if enemy gets hit with player projectile, it should die.
        }
    }

}
