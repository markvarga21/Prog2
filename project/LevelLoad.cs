using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoad : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 2f;

    LanguageSettings langSet;
    string language;

    public Text loadingText;

    void Start()
    {
        // language settings
        langSet = GameObject.FindWithTag("LanguageSettings").GetComponent<LanguageSettings>();
        language = langSet.currentLanguage;
        SetupLanguage();

    }

    void SetupLanguage()
    {
        switch (language)
        {
            case "EN":
                loadingText.text = "Loading...";
                break;
            case "HU":
                loadingText.text = "Betöltés...";
                break;
            default:
                Debug.Log("No language settings in LevelLoad.cs script!");
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if(collisionGameObject.tag == "Player")
        {
            SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.LevelComplete);
            LoadNextScene();
            
        }
    }

    void LoadNextScene()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        // Playing the animation
        transition.SetTrigger("Start");

        // Wait for the animation to stop
        yield return new WaitForSeconds(transitionTime);

        // Load scene
        SceneManager.LoadScene(levelIndex);
    }
}
