using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private List<string> Items;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddItem(GameObject item)
    {
        Items.Add(item.GetComponent<PickableObject>().ObjectName);
    }

}
