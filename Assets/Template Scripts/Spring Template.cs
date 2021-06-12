using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringTemplate : MonoBehaviour
{
    [SerializeField] private float spring_force = 5f; // force of spring
    [SerializeField] private bool up = true; // direction of spring

    private Vector2 direction;
    private Rigidbody2D obj; // Object that hits our spring 

    // Start is called before the first frame update
    void Start()
    {

        // Check if the user set the 'up' to true (want spring to shoot us upwards instead of downwards).
        // If so, then we should change our direction to a unit vector that points directly up.
        // Otherwise, change it to a unit vector that points directly down.
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        obj = collision.gameObject.GetComponent<Rigidbody2D>();
        obj.velocity = new Vector2(0, 0); // freeze the player before they bounce, more cartoony
       
        // CODE one line that adjusts the velocity of the object that hits the spring.
        // The new velocity of the object should be a vector with direction 'direction and scaled
        // by spring_force
    }

}
