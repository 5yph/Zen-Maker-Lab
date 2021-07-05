using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    Rigidbody2D platform;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifespan = 3f;

    // Start is called before the first frame update
    void Start()
    {
        platform = GetComponent<Rigidbody2D>();
        platform.velocity = new Vector2(0, speed);
        Destroy(gameObject, lifespan);
    }
}
