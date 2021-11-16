using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlateManager : MonoBehaviour
{
    private GameObject player;

    // movement plates
    public GameObject movementPlateEN;
    public GameObject movementPlateHU;
    // movement trigger objects
    public GameObject movementTrig1;
    public GameObject movementTrig2;

    // attack plates
    public GameObject attackPlateEN;
    public GameObject attackPlateHU;
    // attack trigger objects
    public GameObject attackTrig1;
    public GameObject attackTrig2;

    // potion plates
    public GameObject potionPlateEN;
    public GameObject potionPlateHU;
    // potion trigger objects
    public GameObject potionTrig1;
    public GameObject potionTrig2;

    // language preferences
    LanguageSettings langSet; // KIVENNI A COMMENTET A VEGEN
    string language;

    void Start()
    {
        // referencing player
        player = GameObject.FindWithTag("Player");
        
        // language settings
        langSet = GameObject.FindWithTag("LanguageSettings").GetComponent<LanguageSettings>();
        language = langSet.currentLanguage;
    }

    
    void Update()
    {
        switch (language)
        {
            case "EN":
                if (betweenThese(movementTrig1, movementTrig2))
                {
                    movementPlateEN.SetActive(true);
                    movementPlateHU.SetActive(false);
                } else movementPlateEN.SetActive(false);

                if (betweenThese(attackTrig1, attackTrig2))
                {
                    attackPlateEN.SetActive(true);
                    attackPlateHU.SetActive(false);
                } else attackPlateEN.SetActive(false);

                if (betweenThese(potionTrig1, potionTrig2))
                {
                    potionPlateEN.SetActive(true);
                    potionPlateHU.SetActive(false);
                } else potionPlateEN.SetActive(false);

                break;
            case "HU":
                if (betweenThese(movementTrig1, movementTrig2))
                {
                    movementPlateHU.SetActive(true);
                    movementPlateEN.SetActive(false);
                } else movementPlateHU.SetActive(false);

                if (betweenThese(attackTrig1, attackTrig2))
                {
                    attackPlateHU.SetActive(true);
                    attackPlateEN.SetActive(false);
                } else attackPlateHU.SetActive(false);

                if (betweenThese(potionTrig1, potionTrig2))
                {
                    potionPlateHU.SetActive(true);
                    potionPlateEN.SetActive(false);
                } else potionPlateHU.SetActive(false);
                break;
            default:
                Debug.Log("No language setting detected!");
                break;
        }
    }

    bool betweenThese(GameObject tr1, GameObject tr2)
    {
        float tr1X = tr1.transform.position.x;
        float tr2X = tr2.transform.position.x;

        float playerX = player.transform.position.x;

        if (tr1X <= playerX && tr2X >= playerX) return true;
        return false;
    }
    
}
