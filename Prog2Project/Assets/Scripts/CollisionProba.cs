using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionProba : MonoBehaviour
{
    
    GameObject textHolder;

    WeaponManager proba = null; // we do not have to reference it bcs we are not handling sprites, only values
    PlayerStats playerStats = null; // these are all scripts
    public HealthBar healthBar;

    public int healthPotionPower = 20;

    void Start()
    {
        textHolder = GameObject.FindWithTag("WeaponAddedText");
        // megkeressuk a scripteket
        proba = GameObject.Find("GunHolder").GetComponent<WeaponManager>();
        playerStats = GameObject.Find("proba player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {

        // weapon pickup
        string tag = collider.gameObject.tag.ToString();

        if (tag.Substring(0, tag.Length-1) == "enable_gun") // engedelyez +1 elore elkeszitett fegyver hasznalatat
        {
            proba.maxLen++;

            DisplayTheNewWeapon(collider.gameObject);

            // adding the new weapon to the inventory
            // letting the player to choose from more weapons than default
        }

        // health potion pickup
        if (tag == "HealthPotionTag")
        {
            int currentHP = playerStats.currentHP;
            currentHP += healthPotionPower;
            healthBar.SetHealth(currentHP);
        }

        // kiiratasok
        //string maxGunIndex = tag.Substring(0, tag.Length-1);
        //Debug.Log("Collision with " + collider.gameObject.name + " with the tag of: " + collider.gameObject.tag + "\n" + maxGunIndex);
        Destroy(collider.gameObject);
    }


    void DisplayTheNewWeapon(GameObject weapon)
    {
        string gunName = weapon.name;

        textHolder.GetComponent<Text>().text = gunName + " added to your inventory!";

        Destroy(textHolder, 3f);
    }


}
