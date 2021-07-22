using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitch : MonoBehaviour
{
    public GameObject player; // keep track of player for transform
    public Canvas SwitchMenu; // menu that gets pulled up to switch levels

    [HideInInspector] public float pX; // positions of player in previous scene
    [HideInInspector] public float pY;
    [HideInInspector] public float pZ;

    // Start is called before the first frame update
    void Start()
    {
        SwitchMenu.enabled = false;
        if (PlayerPrefs.GetInt("Saved") == 1)
        {
            pX = PlayerPrefs.GetFloat("p_x");
            pY = PlayerPrefs.GetFloat("p_y");
            pZ = PlayerPrefs.GetFloat("p_z");

            transform.position = new Vector3(pX, pY, pZ);

            // Reset, so that the save will be used only once
            PlayerPrefs.SetInt("Saved", 0);
            PlayerPrefs.Save();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SwitchMenu.enabled = !SwitchMenu.enabled;
        }
    }

    public void ChangeLevel(string level)
    {
        PlayerPrefs.SetFloat("p_x", transform.position.x);
        PlayerPrefs.SetFloat("p_y", transform.position.y);
        PlayerPrefs.SetFloat("p_z", transform.position.z);

        PlayerPrefs.SetInt("Saved", 1);
        // You need to actually save the values!
        PlayerPrefs.Save();

        if (level != SceneManager.GetActiveScene().name)
        {
            // if we are not trying to load the level we are already in
            SceneManager.LoadScene(level);
        }


    }

}
