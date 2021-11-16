using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionProba : MonoBehaviour
{
    
    PotionManager potionManager;

    GameObject textHolder;

    InventoryManager invMananager;

    PlayerStats playerStats;

    EnemyStats enemyStatsScript;

    // for checking used slot no.
    int slotsUsed;

    // language preferences
    LanguageSettings langSet; // KIVENNI A COMMENTET A VEGEN
    string language;

    // item pickup animation loading
    public GameObject pickupAnimation;

    void Start()
    {
        // potion manager reference
        potionManager = GameObject.Find("PotionHeaders").GetComponent<PotionManager>();

        // text holder reference
        textHolder = GameObject.FindWithTag("WeaponAddedText");

        // gun adding v2
        invMananager = GameObject.Find("Inventory").GetComponent<InventoryManager>();

        // referencing player stat script
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();

        // language settings
        langSet = GameObject.FindWithTag("LanguageSettings").GetComponent<LanguageSettings>();
        language = langSet.currentLanguage;

        // referencing eneymstats script
        enemyStatsScript = GameObject.FindWithTag("Enemy").GetComponent<EnemyStats>();

    }

    void Update()
    {
        slotsUsed = invMananager.slotsUsed;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        // weapon pickup v2
        // from arround the map not from the chest
        string tag = collider.gameObject.tag.ToString();
        if (tag.Substring(0, tag.Length-1) == "enable_gun")
        {
            SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.pickupSound);
            if (slotsUsed < 5) {
                // playing pickup animation
                GameObject effect = Instantiate(pickupAnimation, collider.transform.position, Quaternion.identity); // quaternion.identity == syntax for no rotation
                Destroy(effect, 0.5f);

                // getting the name of the gun
                string gunName = collider.gameObject.name.ToString();

                // extracting gun index out of it
                string gunIndexString = gunName.Substring(3);
                int gunIndex = Int32.Parse(gunIndexString); // mivel a gun nevet fogjuk hasznalni a tovabbiakban
                //Debug.Log("Gun" + gunIndex + " has to be added to inventory!");
                invMananager.AddToInventory(gunIndex);
                Destroy(collider.gameObject); // ladasnal javitani kell, mivel a destroy miatt nem lehet felvenni a fegyvert
                //collider.gameObject.SetActive(false);
                StartCoroutine(DisplayTheNewWeapon(collider.gameObject));
            } else StartCoroutine(InventoryFullMsg());
        }


        // health potion pickup
        if (tag == "HealthPotionTag")
        {
            GameObject effect = Instantiate(pickupAnimation, collider.transform.position, Quaternion.identity); // quaternion.identity == syntax for no rotation
            Destroy(effect, 0.5f);

            SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.pickupSound);
            potionManager.HealthPotionActivated();
            Destroy(collider.gameObject); // azert van ide rakva, hogy csak a potion, illetve a fegyverek eseten torolje ki a GameObject-et
            // hiszen ha minden colision eseten torli, akkor nem mukodik a IEnumerator function
        }

        // power potion pickup
        if (tag == "PowerPotionTag")
        {
            GameObject effect = Instantiate(pickupAnimation, collider.transform.position, Quaternion.identity); // quaternion.identity == syntax for no rotation
            Destroy(effect, 0.5f);

            SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.pickupSound);
            potionManager.PowerPotionActivated();
            Destroy(collider.gameObject);
        }

        // speed potion pickup
        if (tag == "SpeedPotionTag")
        {
            GameObject effect = Instantiate(pickupAnimation, collider.transform.position, Quaternion.identity); // quaternion.identity == syntax for no rotation
            Destroy(effect, 0.5f);

            SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.pickupSound);
            potionManager.SpeedPotionActivated();
            Destroy(collider.gameObject);
        }

        if (tag == "coin")
        {
            GameObject effect = Instantiate(pickupAnimation, collider.transform.position, Quaternion.identity); // quaternion.identity == syntax for no rotation
            Destroy(effect, 0.5f);
            
            SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.coinAdded);
            playerStats.AddMoneyOnCoinCollision();
            Destroy(collider.gameObject);
        }


        //Destroy(collider.gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            playerStats.TakeDamage(enemyStatsScript.enemyDamage);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            playerStats.TakeDamage(enemyStatsScript.enemyDamage);
        }
    }


    IEnumerator DisplayTheNewWeapon(GameObject weapon)
    {
        string gunName = weapon.name;

        if (language == "EN" || language == "")
            textHolder.GetComponent<Text>().text = gunName + " added to your inventory!";

        if (language == "HU")
            textHolder.GetComponent<Text>().text = "A " + gunName + " hozzá lett adva a leltárodhoz!";

        yield return new WaitForSeconds(3f);
        textHolder.GetComponent<Text>().text = "";        
        
    }

    // not working yet
    IEnumerator InventoryFullMsg()
    {
        SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.errorSound);
        Debug.Log("inventory is full");
        textHolder.GetComponent<Text>().color = Color.red;
        if (language == "EN" || language == "")
            textHolder.GetComponent<Text>().text = "Inventory is full!";

        if (language == "HU")
            textHolder.GetComponent<Text>().text = "Tele van az inventory!";

        yield return new WaitForSeconds(3f);

        textHolder.GetComponent<Text>().text = "";        
        textHolder.GetComponent<Text>().color = Color.white;
    }

    IEnumerator DestroyGun(GameObject gun)
    {
        yield return new WaitForSeconds(2f);
        Destroy(gun);
    }

}

