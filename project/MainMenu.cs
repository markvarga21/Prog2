using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    // gombok referencelese, nyelvvaltas celjabol
    // [almenuneve]/[adott button neve]btn

    // SETTINGS MENU
    public Button SettingsBackBtn;
    public Text SettingSettingTxt;
    public Text SettingsLanguageTxt;
    public Text AudioLanguageTxt;
    public Text GraphicsText;
    public Text FullscreenText;

    // MAIN MENU
    public Button MainPlayBtn;
    public Button MainSettingsBtn;
    public Button MainQuitBtn;

    // ABOUT MENU
    public Button AboutBackBtn;
    public Text AboutHeaderTxt;
    public Text AboutBodyTxt;

    // actual language
    public string gameLanguage;
    LanguageSettings langSet;

    void Start()
    {
        langSet = GameObject.FindWithTag("LanguageSettings").GetComponent<LanguageSettings>();
    }

    void Update()
    {
        langSet.currentLanguage = gameLanguage;
    }


    public void ExitButton()
    {
        Application.Quit();
        // developer windowban nem latjuk, ezert h megbiztosodjunk rola, kiiratjuk a konzolra
        Debug.Log("Kileptunk a jatekbol!");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");

    }

    public void AnyToHu()
    {
        // setting up the actual language
        gameLanguage = "HU";

        // SETTINGS MENU
        SettingsBackBtn.GetComponentInChildren<Text>().text = "Vissza";
        SettingSettingTxt.text = "Beallitasok";
        SettingsLanguageTxt.text = "Nyelv";
        AudioLanguageTxt.text = "Hang";
        GraphicsText.text = "Grafika";
        FullscreenText.text = "Teljes képernyő";

        // MAIN MENU
        MainPlayBtn.GetComponentInChildren<Text>().text = "Jatek";
        MainSettingsBtn.GetComponentInChildren<Text>().text = "Beallitasok";
        MainQuitBtn.GetComponentInChildren<Text>().text = "Kilepes";

        // ABOUT MENU
        AboutBackBtn.GetComponentInChildren<Text>().text = "Vissza";
        AboutHeaderTxt.text = "About";
        AboutBodyTxt.text = "Ebben a jatekban az a cél,\nhogy a rokat a celba juttasuk, majd megolve a vegso bosst\nelnyerjuk melto jutalmunkat, a hos fox cimet! Utkozben szamos akadaly lesz, ellensegek, mergezo tuskek,\nviszont szamos elonyhoz is juthat a foszereplo, mint peldaul bajitalok es szamos fegyverek.\nEz lenne tomoren a jatek.\nJo szorakozast hozza!";
    }

    public void AnyToEng()
    {
        // setting up the actual language
        gameLanguage = "EN";

        // SETTINGS MENU
        SettingsBackBtn.GetComponentInChildren<Text>().text = "Back";
        SettingSettingTxt.text = "Settings";
        SettingsLanguageTxt.text = "Language";
        AudioLanguageTxt.text = "Audio";
        GraphicsText.text = "Graphics";
        FullscreenText.text = "Fullscreen";


        // MAIN MENU
        MainPlayBtn.GetComponentInChildren<Text>().text = "Play";
        MainSettingsBtn.GetComponentInChildren<Text>().text = "Settings";
        MainQuitBtn.GetComponentInChildren<Text>().text = "Quit";

        // ABOUT MENU
        AboutBackBtn.GetComponentInChildren<Text>().text = "Back";
        AboutHeaderTxt.text = "About";
        AboutBodyTxt.text = "The purpose of this game is,\n that the fox has to reach the final destination, and after killing the final boss\nhe gets his prize, the hero fox title! Along the way, there will be many obstacles, enemies, poisionous spikes, \nbut he'll got some help too, by collecting and using potions and different weapons.\nThis is the game in a few words.\nEnjoy the journey! :D";
    }
}
