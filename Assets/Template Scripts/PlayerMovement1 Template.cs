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
        // TASK #1

        // We initialized 'rb' (Rigidbody2D object).
        // However, this object isn't actually assigned to anything yet, it's basically empty!
        // Assign these to the corresponding components of our character using GetComponent<>()
    }

    // Update is called once per frame
    void Update()
    {   // detect input in Update() so we don't miss any inputs

        float x_component = Input.GetAxisRaw("Horizontal");
        // Input.GetAxisRaw() returns 1 if going right, -1 if going left

        // TASK #2

        // UNCOMMENT BELOW
        // direction = new Vector2( What should the x-component be?, rb.velocity.y);

        // direction is a 2D vector that points to where we are moving
        // the y-component is not altered, we will only alter when we jump
        // don't forget to take 'movespeed' into account.

        // TASK #3

        /* Write a conditional statement that checks if the player pressed Jump.
           If they did, record that using our try_jump boolean variable, so we can remember
           it in the next physics update.
         
           Hint: Input.GetButtonDown("Jump") returns true if the player presses the jump key.
         */
    }

    private void FixedUpdate()
    {   // for physics updates

        // TASK #4

        // Call the Move function here (don't forget the parameter)

        // Check if the user is trying to jump, if so call the Jump function here
    }

    private void Move(Vector2 direction)
    {

        // TASK #5

        // Adjust the velocity of our rigid body to match the new 'direction' velocity
        // Hint: Look at Unity documentation to find out what determines the velocity of our rigidbody
    }

    private void Jump()
    {
        rb.velocity = new Vector2(0, jumpspeed);
        try_jump = false;
    }

}
