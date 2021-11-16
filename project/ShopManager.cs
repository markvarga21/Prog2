using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    
    // shop dialog
    public GameObject shopDialogueEN;
    public GameObject shopDialogueHU;

    // milyen hamar triggerelodjon maga az action, attol fuggoen hogy a player a 'triggerOffset' tavolsagaban van
    public float triggerOffset = 1f;

    // shop UI
    public GameObject shopUI;
    public bool playerIsShopping = false;

    // texts
    public Text mainDialogue;
    public Text healthDialogue;
    public Text speedDialogue;
    public Text powerDialogue;


    // language preferences
    LanguageSettings langSet; 
    string language;

    void Start()
    {
        // language settings
        langSet = GameObject.FindWithTag("LanguageSettings").GetComponent<LanguageSettings>();
        language = langSet.currentLanguage;

        //shopUI.SetActive(false);

        // setting up language settings
        SetupLanguage();

    }

    void SetupLanguage()
    {
        switch(language)
        {
            case "EN":
                mainDialogue.text = "Shop";
                healthDialogue.text = "Health";
                speedDialogue.text = "Speed";
                powerDialogue.text = "Power";
                break;
            case "HU":
                mainDialogue.text = "Bolt";
                healthDialogue.text = "Életerő";
                speedDialogue.text = "Sebesség";
                powerDialogue.text = "Erő";
                break;
            default:
                Debug.Log("No language settings!");
                break;
        }
    }

    void Update()
    {
        // only for debugging purposes
        if (Input.GetKeyDown("l"))
            Debug.Log("Player x position" + GameObject.FindWithTag("Player").transform.position.x + "\nAnd house position X is" + this.transform.position.x);

        // extracting player's actual position
        float playerPosX = GameObject.FindWithTag("Player").transform.position.x;

        // displaying shop dialogue to open it
        if (PlayerIsAroundShop(playerPosX)) {
            switch (language)
            {
                case "EN":
                    shopDialogueEN.SetActive(true);
                    break;
                case "HU":
                    shopDialogueHU.SetActive(true);
                    break;
                default:
                    Debug.Log("No language setting in ShopManager.cs script!");
                    break;
            }
        } else {
            switch (language)
            {
                case "EN":
                    shopDialogueEN.SetActive(false);
                    break;
                case "HU":
                    shopDialogueHU.SetActive(false);
                    break;
                default:
                    Debug.Log("No language setting in ShopManager.cs script!");
                    break;
            }
            HideShopUI();
        } 
        

        // opening shop (UI)
        if (Input.GetKeyDown("b") && PlayerIsAroundShop(playerPosX))
        {
            // not working yet
            if (playerIsShopping) {
                HideShopUI();
            }
            else {
                LoadShopUI();
            }
        } 

    }

    void LoadShopUI()
    {
        shopUI.SetActive(true);
        playerIsShopping = true;
    }

    void HideShopUI()
    {
        shopUI.SetActive(false);
        playerIsShopping = false;
        
    }

    bool PlayerIsAroundShop(float playerX)
    {
        float shopX = this.transform.position.x;
        if (playerX >= shopX - triggerOffset && playerX <= shopX + triggerOffset)
            return true;
        return false;
    }
}
