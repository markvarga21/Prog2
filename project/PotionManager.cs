using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PotionManager : MonoBehaviour
{
    // error message holder
    GameObject textHolder;

    // text objects
    GameObject HealthPotionCounter;
    GameObject SpeedPotionCounter;
    GameObject PowerPotionCounter;

    // current potion counters initialization (int)
    public int currentHealPotionAvailable;
    public int currentSpeedPotionAvailable;
    public int currentPowerPotionAvailable; 

    // player stats
    PlayerStats playerStats = null; // these are all scripts

    // health related stuff
    public HealthBar healthBar;
    public int healthPotionPower = 20;

    // speed increase informations
    public float speedPotionTime = 5f;
    public float speedPotionPower = 5f;
    float defaultSpeed;

    // power increase informations
    public float powerPotionTime = 5f;
    public int powerPotionPower = 20;
    int defaultPower = 20;

    // language preferences
    LanguageSettings langSet; 
    string language;

    // potion counter
    public GameObject potionTimer;
    public Slider slider;

    // not allowing the player to use more than 1 potion from a certain type
    bool speedIsInUse = false;
    bool powerIsInUse = false;

    
    void Start()
    {
        // text holder reference
        textHolder = GameObject.FindWithTag("WeaponAddedText");

        // finding text objects containing the current potion count
        HealthPotionCounter = GameObject.Find("HealthPotionCounter");
        SpeedPotionCounter = GameObject.Find("SpeedPotionCounter");
        PowerPotionCounter = GameObject.Find("PowerPotionCounter");

        // initializing all counters (string content) to zero just to make sure
        HealthPotionCounter.GetComponent<Text>().text = "0";
        SpeedPotionCounter.GetComponent<Text>().text = "0";
        PowerPotionCounter.GetComponent<Text>().text = "0";

        // finding the script containing the player's stats
        playerStats = GameObject.Find("proba player").GetComponent<PlayerStats>();

        // default speed assigning
        defaultSpeed = playerStats.playerSpeed;

        // language settings
        langSet = GameObject.FindWithTag("LanguageSettings").GetComponent<LanguageSettings>();
        language = langSet.currentLanguage;

        // potion timer
        potionTimer.SetActive(false);
        //slider = GameObject.Find("PotionTimer").GetComponent<Slider>();
        slider.minValue = 0;
    }

    
    void Update()
    {
        // updating current health in every frame
        currentHealPotionAvailable = Int32.Parse(HealthPotionCounter.GetComponent<Text>().text);
        currentSpeedPotionAvailable = Int32.Parse(SpeedPotionCounter.GetComponent<Text>().text);
        currentPowerPotionAvailable = Int32.Parse(PowerPotionCounter.GetComponent<Text>().text);


        // USING POTIONS
        if (Input.GetKeyDown("z"))
        {
            Heal();
        }

        if (Input.GetKeyDown("x"))
        {
            IncreaseSpeed();
        }

        if (Input.GetKeyDown("c"))
        {
            IncreasePower();
        }
    }

    // itt cska activalunk, triggerelunk functionoket, csak a szamlalot noveljuk
    public void HealthPotionActivated() // with public we can acces it from other scripts too
    {
        IncreaseHealPotionCount();
    }

    public void PowerPotionActivated() // with public we can acces it from other scripts too
    {
        IncreasePowerPotionCount();
    }

    public void SpeedPotionActivated() // with public we can acces it from other scripts too
    {
        IncreaseSpeedPotionCount();
    }



    // HEAL POTION
    void IncreaseHealPotionCount()
    {           
        HealthPotionCounter.GetComponent<Text>().text = (currentHealPotionAvailable+1).ToString();
    }

    void DecreaseHealPotionCount()
    {
        HealthPotionCounter.GetComponent<Text>().text = (currentHealPotionAvailable-1).ToString();
    }

    public void Heal()
    {

        if (currentHealPotionAvailable > 0) {
            SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.healPotionSound);
            // reducing the players health intiger and also modifying the UI element
            playerStats.currentHP += healthPotionPower;
            healthBar.SetHealth(playerStats.currentHP);

            // extracting HP potion count
            DecreaseHealPotionCount();
        }  else StartCoroutine(EmptyCountMsg("heal"));
    }





    // SPEED POTION
    void IncreaseSpeedPotionCount()
    {           
        SpeedPotionCounter.GetComponent<Text>().text = (currentSpeedPotionAvailable+1).ToString();
    }

    void DecreaseSpeedPotionCount()
    {
        SpeedPotionCounter.GetComponent<Text>().text = (currentSpeedPotionAvailable-1).ToString();
    }

    void IncreaseSpeed()
    {
        if (!speedIsInUse) {
            if (currentSpeedPotionAvailable > 0) {
                SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.healPotionSound);
                DecreaseSpeedPotionCount();
                StartCoroutine(IncreaseSpeedPeriod(speedPotionTime));
                
            } else StartCoroutine(EmptyCountMsg("speed"));
        } else Debug.Log("A speed potion is already in use!");
    }

    IEnumerator IncreaseSpeedPeriod(float time)
    {
        //Debug.Log("We increased the players speed with " + speedPotionPower);

        speedIsInUse = true;
        potionTimer.SetActive(true);
        playerStats.playerSpeed += speedPotionPower;
        slider.maxValue = time;

        while (time >= 0) {
            slider.value = time;
            yield return new WaitForSeconds(1f);
            time--;
        }

        potionTimer.SetActive(false);
        playerStats.playerSpeed = defaultSpeed;
        speedIsInUse = false;
    }




    // POWER POTION
    void IncreasePowerPotionCount()
    {           
        PowerPotionCounter.GetComponent<Text>().text = (currentPowerPotionAvailable+1).ToString();
    }

    void DecreasePowerPotionCount()
    {
        PowerPotionCounter.GetComponent<Text>().text = (currentPowerPotionAvailable-1).ToString();
    }

    void IncreasePower()
    {
       if (!powerIsInUse) {
            if (currentPowerPotionAvailable > 0) {
                SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.healPotionSound);
                DecreasePowerPotionCount();
                StartCoroutine(IncreasePowerPeriod(powerPotionTime));
                
            } else StartCoroutine(EmptyCountMsg("power"));
        } else Debug.Log("A power potion is already in use!");
    }

    IEnumerator IncreasePowerPeriod(float time)
    {
        powerIsInUse = true;
        potionTimer.SetActive(true);
        playerStats.PlayerDamage += powerPotionPower;
        slider.maxValue = time;

        while (time >= 0) {
            slider.value = time;
            yield return new WaitForSeconds(1f);
            time--;
        }

        potionTimer.SetActive(false);
        playerStats.PlayerDamage = defaultPower;
        powerIsInUse = false;
    }


    IEnumerator EmptyCountMsg(string potion)
    {
        SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.errorSound);
        textHolder.GetComponent<Text>().color = Color.red;
        if (language == "EN" || language == "")
            textHolder.GetComponent<Text>().text = "You don't have any " + potion + " potion available!";

        if (language == "HU")
            textHolder.GetComponent<Text>().text = "Nincs több " + potion + " bájitalod!";

        yield return new WaitForSeconds(3f);

        textHolder.GetComponent<Text>().text = ""; 
        textHolder.GetComponent<Text>().color = Color.white;       
        
    }
}
