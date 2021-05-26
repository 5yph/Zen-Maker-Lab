using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer music;
    public Slider volSlider;

    /* void Start()
    {
        
    } */

    public void SetVol(float value)
    {
        music.SetFloat("MusicVol", Mathf.Log10(value) * 20);
        // The "MusicVol" string should exactly match whatever you renamed your exposed parameter
        // of the music group to.
        // The number 20 is mathematically derived from our wanted Decibel range (-80 to 0 dB).
    }
}
