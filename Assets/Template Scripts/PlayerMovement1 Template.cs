using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TEMPLATE FOR FIRST VERSION -- START WITH THIS

public class PlayerMovement1Template : MonoBehaviour // Ensure name matches script file name
{
    private Rigidbody2D rb;

    [SerializeField] private float movespeed;
    [SerializeField] private float jumpspeed;

    private Vector2 direction; // movement direction
    private bool try_jump = false; // is the player trying to jump?

    // Start is called before the first frame update
    void Start()
    {
        // We initialized 'rb' (Rigidbody2D object).
        // However, this object isn't actually assigned to anything yet, they're basically empty!
        // Assign these to the corresponding components of our character using GetComponent<>()
    }

    // Update is called once per frame
    void Update()
    {   // detect input in Update() so we don't miss any inputs

        float x_component = Input.GetAxisRaw("Horizontal");
        // Input.GetAxisRaw() returns 1 if going right, -1 if going left

        direction = new Vector2(/* What should the x-component be?*/, rb.velocity.y);

        // direction is a 2D vector that points to where we are moving
        // the y-component is not altered, we will only alter when we jump

        /* Write a conditional statement that checks if the player pressed Jump.
           If they did, record that using our try_jump boolean variable.
         
           Hint: Look at Input.GetButtonDown in the Unity documentation
         */
    }

    private void FixedUpdate()
    {   // for physics updates

        // Call the Move function here (don't forget the parameter)

        // Check if the user is trying to jump, if so call the Jump function here
    }

    private void Move(Vector2 direction)
    {
        // Adjust the velocity of our rigid body to match the new 'direction' velocity
        // Hint: rb.velocity represents the velocity of our rigid body
    }

    private void Jump()
    {
        rb.velocity = new Vector2(0, jumpspeed);
        try_jump = false;
    }

}
