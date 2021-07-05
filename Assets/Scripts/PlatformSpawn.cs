using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawn : MonoBehaviour
{
    public GameObject platform;

    [SerializeField] private float spawnrate = 1f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnPlatform", spawnrate, spawnrate);
        // We call SpawnPlatform() immediately, and repeatedly call it at a fixed time interval
    }

    // Update is called once per frame

    void SpawnPlatform()
    {
        Instantiate(platform, gameObject.transform.position, platform.transform.rotation);
    }
}
