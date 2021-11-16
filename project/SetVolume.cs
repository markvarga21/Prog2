using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetLevel(float sliderValue)
    {
        // the first one is the name of the exposed parameter
        // azert kell a logaritmikus, mert decibelekrol beszelunk
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20); 
    }
}
