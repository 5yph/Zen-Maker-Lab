using UnityEngine;

public class AdjustSourceVol : MonoBehaviour
{
    AudioSource MyAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        MyAudioSource = this.GetComponent<AudioSource>();
        
        MyAudioSource.volume = 0.2f;
        // Change the float value to any decimal in order to adjust clip volume
        // (don't forget the 'f' at the end)
        // Recommended to only lower it due to clipping. 
    }

}
