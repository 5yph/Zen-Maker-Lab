using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Animator animator;
    public bool activated = false; // did we clear the checkpoint already

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !activated)
        {
            animator.SetTrigger("Activated");
            activated = true;
            collision.gameObject.GetComponent<Damage>().Spawn = transform.GetChild(0).transform;
            // Set the spawn point of our player to the respawn point of this checkpoint
            // Give that the respawn point is the first child of this checkpoint
        }
    }
}
