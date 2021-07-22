using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    public string ObjectName; // the name of the object
    public GameObject player; // the player that uses these items
    private ItemManager inventory; // tracks the items that the player has
    private bool added = false; // was this item added already

    [HideInInspector] public bool has_object = false; // is the object collected by the player

    // Start is called before the first frame update
    void Start()
    {
        inventory = player.GetComponent<ItemManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !added)
        {
            // if the player collects the key
            has_object = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = false; // disable sprite
            inventory.AddItem(gameObject);
            added = true;
        }
    }

}
