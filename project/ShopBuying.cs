using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuying : MonoBehaviour
{
    public int healthPotionCost = 20;
    public int powerPotionCost = 40;
    public int speedPotionCost = 30;

    PlayerStats playerStats;
    PotionManager shopManager;

    public Text text;

    // language preferences
    LanguageSettings langSet;
    string language;

    void Start()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        shopManager = GameObject.Find("PotionHeaders").GetComponent<PotionManager>();

        // language settings
        langSet = GameObject.FindWithTag("LanguageSettings").GetComponent<LanguageSettings>();
        language = langSet.currentLanguage;
    }

    public void HealthPotionPurchase()
    {
        SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.Click);
        if (playerStats.playerMoney - healthPotionCost >= 0) {
            playerStats.playerMoney -= healthPotionCost;
            shopManager.HealthPotionActivated();
        }
        else 
            StartCoroutine(ErrorMsg());
        
    }

    public void SpeedPotionPurchase()
    {
        SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.Click);
        if (playerStats.playerMoney - speedPotionCost >= 0) {
            playerStats.playerMoney -= speedPotionCost;
            shopManager.SpeedPotionActivated();
        }
        else 
            StartCoroutine(ErrorMsg());
    }

    public void PowerPotionPurchase()
    {
        SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.Click);
        if (playerStats.playerMoney - powerPotionCost >= 0) {
            playerStats.playerMoney -= powerPotionCost;
            shopManager.PowerPotionActivated();
        }
        else 
            StartCoroutine(ErrorMsg());
    }

    IEnumerator ErrorMsg()
    {
        SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.errorSound);
        if (language == "EN")
            text.text = "Not enough money!";
        if (language == "HU")
            text.text = "Nincs elég pénzed!";
        yield return new WaitForSeconds(3f);
        text.text = "";
    }
}
