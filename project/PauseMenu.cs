using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public bool GameIsPaused = false; // static bcs we do not want to reference it, we just want to check it from other scripts 
    public GameObject pauseMenuUI;

    public GameObject player;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused) {
                Resume();       
            } 
            else {
                Pause();
            }
        }
    }

    public void Resume()
    {   
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // 1 == normal rate 
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        // freezing the time
        Time.timeScale = 0f; // with this we can create slow motion too
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
        // destroy the player
        Destroy(player);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game!");
        Application.Quit();
    }
}
