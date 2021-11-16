using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSettings : MonoBehaviour
{

    public string currentLanguage = "EN";

    void Start()
    {
        currentLanguage = "EN";
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        
    }
}
