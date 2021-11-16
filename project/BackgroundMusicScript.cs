using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicScript : MonoBehaviour
{
    public static BackgroundMusicScript bgInsntace;

    void Awake()
    {
        // azert kell ez ide, hogy ne summonoljunk egy masik background instance-ot
        if (bgInsntace != null && bgInsntace != this) 
        {
            Destroy(this.gameObject);
            return;
        }

        bgInsntace = this;
        DontDestroyOnLoad(this);
    }
}
