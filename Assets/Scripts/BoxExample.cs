using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxExample : MonoBehaviour
{
    Transform trans;
    Rigidbody2D rb;
    private float x_component = 50;
    private float y_component = 0;

    private bool moveright = true;
    private bool moveleft = false;
    private bool moveup = false;
    private bool movedown = false;

    int count = 0;
    private bool completesquare = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (completesquare)
        {
            trans.position = new Vector3(-100, -99, 0);
            completesquare = false;
        }

        if (moveright)
        {
            x_component = 50;
            y_component = 0;
        } else if (moveleft)
        {
            x_component = -50;
            y_component = 0;
        } else if (moveup)
        {
            x_component = 0;
            y_component = 50;
        } else if (movedown) {
            x_component = 0;
            y_component = -50;
        }

        rb.velocity = new Vector2(x_component, y_component);

        if (trans.position.x > 100)
        {
            moveup = true;
            moveright = false;
        }
        
        if (trans.position.y > 100)
        {
            moveup = false;
            moveleft = true;
        } 
        
        if (trans.position.x < -100)
        {
            moveleft = false;
            movedown = true;
        } 
        
        if (trans.position.y < -100)
        {
            movedown = false;
            moveright = true;
            completesquare = true;
        }
    }
}
