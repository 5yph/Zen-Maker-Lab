using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VolumeTextUpdate : MonoBehaviour
{
    public TextMeshProUGUI volText; // create new text object

    // Start is called before the first frame update
    void Start()
    {
        volText = this.GetComponent<TextMeshProUGUI>(); // set volText to the TMP component
    }

    public void UpdateText(float value) // 'value' is automatically passed from slider object
    {
        volText.text = "Volume: " + value + "%"; // change volume text
    }

}
