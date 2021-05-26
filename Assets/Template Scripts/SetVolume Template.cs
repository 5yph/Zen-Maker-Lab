using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolumeTemplate : MonoBehaviour
{
    public AudioMixer music;
    public Slider volSlider;

    void Start()
    {
        // music.SetFloat(|YOUR MIXER GROUP HERE|, Mathf.Log10(0.3f) * 20);
        // set default volume to 30%
    }

    public void SetVol(float value)
    {
        // music.SetFloat(|YOUR MIXER GROUP HERE|, Mathf.Log10(value) * 20);

        // The number 20 is mathematically derived from our wanted Decibel range (-80 to 0 dB).
    }
}
