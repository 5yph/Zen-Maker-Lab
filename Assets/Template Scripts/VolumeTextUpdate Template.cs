using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // for text mesh pro utility

public class VolumeTextUpdateTemplate : MonoBehaviour
{
    public TextMeshProUGUI volText; // create new text object

    // Start is called before the first frame update
    void Start()
    {
        /* volText = ???
        Find a method to automatically instantiate our volText object at runtime.

        Hints: 
        - 'GetComponent<COMPONENT>()' is a method of every game object.
        - 'GetComponent<COMPONENT>()' returns COMPONENT, we can return our TextMeshProUGUI component
        */
    }

    public void UpdateText(float value) // 'value' is automatically passed from slider object
    {
        // volText.text = "Volume: " + |INSERT VALUE HERE| + "%";
        
        /* We could just pass 'value' into that string to print out our text, but value represents
        a very long float (decimal) number, which we don't like. Try using Round() from the Mathf 
        class to combat this.
        Don't forget to convert the decimal value to a percentage before rounding.
        */

    }
}
