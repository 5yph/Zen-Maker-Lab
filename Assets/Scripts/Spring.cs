using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField] private float spring_force = 5f;

    private Rigidbody2D obj; // Object that hits our spring 
    public Vector2 direction; // direction of spring force

    // Start is called before the first frame update
    void Start()
    {
        direction.Normalize();
        Debug.Log(direction * spring_force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        obj = collision.gameObject.GetComponent<Rigidbody2D>();
        obj.velocity = new Vector2(0, 0); // freeze the player before they bounce, more cartoony
        obj.velocity = direction * spring_force;

        if (direction.x != 0)
        {
            // if there is some horizontal movement, need to adjust spring 
            // this is because our movement script directly manipulates horizontal velocity

        }

        Debug.Log(direction * spring_force);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, direction);
    }
}
