using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishUI : MonoBehaviour
{
    public GameObject finalUI; // az image a canvas-ban
    public Text finalUIText; // a text a canvason

    // stablistas animacio
    public GameObject finalUITextObject;
    Animator anim;

    // language preferences
    LanguageSettings langSet;
    string language;

    // audio sources
    GameObject audio;
    GameObject SFX;

    void Start()
    {
        // extracting text animator
        anim = finalUITextObject.GetComponent<Animator>();

        // referencing audios
        SFX = GameObject.Find("SFXManager");
        audio = GameObject.Find("BackgroundMusic");

        // language settings
        langSet = GameObject.FindWithTag("LanguageSettings").GetComponent<LanguageSettings>();
        language = langSet.currentLanguage; 
    }

    



    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            switch(language)
            {
                case "EN":
                    finalUIText.text = "Congratulation!\nYou have completed\n\nFox Adventure\n\ngame!\n\n- Credits: -\n\nMap inspired by Ansimuz\n\nAssets from Unity Asset Store\n\nMusic and other assets from the internet";
                    finalUI.SetActive(true);
                    Debug.Log("Playing final animation");
                    //Time.timeScale = 0f;
                    StartCoroutine(ExitGame());
                    break;
                case "HU":
                    finalUIText.text = "Gratulalok!\nKijatszottad a\n\nFox Adventure\n\njatekot!\n\n- Credits -\n\nPalya inspiracio Ansimuz-tol\n\nAsset-ek a Unity Asset Store-bol\n\nZene es egyeb asset-ek az internetrol";
                    finalUI.SetActive(true);
                    Debug.Log("Playing final animation");
                    //Time.timeScale = 0f;
                    StartCoroutine(ExitGame());
                    break;
                default:
                    Debug.Log("Something went wrong with the final UI!");
                    break;
            }
        }

    }

    IEnumerator ExitGame()
    {
        // disabling all audio
        audio.SetActive(false);
        SFX.SetActive(false);

        // playing stablista
        anim.Play("Base Layer.FinalUIAnimation");
        yield return new WaitForSeconds(20f);
        Application.Quit();
    }

}
