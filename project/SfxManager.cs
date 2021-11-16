using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{

    public AudioSource Audio;

    public AudioClip Click;
    public AudioClip Die;
    public AudioClip LevelComplete;
    public AudioClip Jump;
    public AudioClip coinAdded;
    public AudioClip checkPointSound;
    public AudioClip healPotionSound;
    public AudioClip easterEgg;
    public AudioClip memeExplosion;
    public AudioClip errorSound;
    public AudioClip chestOpenedSound;
    public AudioClip pickupSound;

    // ezt tudjuk hasznalni referencia nelkul a kulso scriptekben
    public static SfxManager sfxInstance;

    void Awake()
    {
        // hogy ne duplazzuk az egyeteket az uj scene-k betoltesekor
        if (sfxInstance != null && sfxInstance != this) 
        {
            Destroy(this.gameObject);
            return;
        }

        sfxInstance = this;
        DontDestroyOnLoad(this);
    }
}

// hivatkozasok a clipekre
// SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.pickupSound);
