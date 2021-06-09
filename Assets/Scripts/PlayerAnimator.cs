using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    PlayerMovement player;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.direction.magnitude < 0.1)
        {
            // not moving
            animator.SetBool("Moving", false);
        } else
        {
            animator.SetBool("Moving", true);
        }
    }
}
