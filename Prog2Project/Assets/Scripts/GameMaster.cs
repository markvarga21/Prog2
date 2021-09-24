using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;
    public Vector2 lastCheckPointPos;


    void Awake() // this happens when the scene loads in
    {
        // this prevents us having multiple gamemasters in a scene
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(instance);
        } else {
            //Destroy(gameObject);
        }
    }
}
