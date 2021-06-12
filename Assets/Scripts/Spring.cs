using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField] private float spring_force = 5f; // force of spring
    [SerializeField] private bool up = true; // direction of spring

    private Vector2 direction;
    private Rigidbody2D obj; // Object that hits our spring 

    // Start is called before the first frame update
    void Start()
    {
        if (up)
        {
            direction = new Vector2(0, 1);
        } else
        {
            direction = new Vector2(0, -1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        obj = collision.gameObject.GetComponent<Rigidbody2D>();
        obj.velocity = new Vector2(0, 0); // freeze the player before they bounce, more cartoony
        obj.velocity = direction * spring_force;
    }
}
