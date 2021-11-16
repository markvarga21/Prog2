using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    // error message text holder
    GameObject textHolder;

    // usable gun list + existence string array
    List<GameObject> usableGuns = new List<GameObject>();
    string[] slotExistence = new string[5];
    public int slotsUsed;

    public int activeStatusGunIndex = -1;

    // image slots for the UI
    public Image[] slots;
    public Image[] guns;
    bool[] televan = new bool[5];

    // the guns default position when they are not in use
    float defaultX = -87.7f;
    float defaultY = -588.2001f;

    // weapon gameobject holder
    public GameObject[] weapons;

    // implement dictionaries

    // language preferences
    LanguageSettings langSet; // KIVENNI A COMMENTET A VEGEN
    string language;

    // active gun highlighting
    public Image highlight;

    // bullet damage informations
    public int gun1Damage = 10;
    public int gun2Damage = 10;
    public int gun3Damage = 10;
    public int gun4Damage = 10;
    public int gun5Damage = 10;
    public int gun6Damage = 10;
    public int gun7Damage = 10;
    public int gun8Damage = 10;

    // bullet counts
    public int[] BulletCounts = {20, 20, 20, 20, 20, 20, 20, 20};

    // bullet counter text
    public Text bulletCounter;

    // melee attack
    public string ftype;
    public GameObject sword;

    void Start()
    {
        // initializing the textHolder object
        textHolder = GameObject.FindWithTag("WeaponAddedText");

        // initializing slot statuses to 0
        for (int i = 0; i < televan.Length; i++)
            televan[i] = false;

        // initializing slot existence with ""
        for (int i = 0; i < slotExistence.Length; i++)
            slotExistence[i] = "";

        // initializing not to equip more than one weapon
        slotsUsed = 0;

        // melee attack inits.
        ftype = "DEFAULT";

        
        // language settings
        langSet = GameObject.FindWithTag("LanguageSettings").GetComponent<LanguageSettings>();
        language = langSet.currentLanguage;
        // KIVENNI A COMMENTET A VEGEN
    }

    
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            ftype = "MELEE";
            DeactivateAllGuns();
            sword.SetActive(true);
        }

        // dropping from inventory
        if (Input.GetKeyDown("q") && activeStatusGunIndex != -1) // csak akkor lehessen droppolni, ha fegyver van a player kezeben
        {
            // removing sprite from inventory slot
            Image sprite = guns[activeStatusGunIndex-1];
            Debug.Log(sprite.name);
            sprite.transform.position = new Vector3(defaultX, defaultY, 0);

            //removing highlight
            highlight.transform.position = new Vector2(defaultX, defaultY);

            // removing from useable guns also
            RemoveFromUsable(activeStatusGunIndex);
            
            
            // nezzuk meg ez a gun melyik slotban van s azt uritsuk ki
            for (int i = 0; i < slotExistence.Length; i++)
            {
                if (slotExistence[i] == "gun" + activeStatusGunIndex) {
                    slotExistence[i] = "";
                    break;
                }
            }


        }
        

        
        if (Input.GetKeyDown("1"))
        {
            // changing fight type and disabling sword
            ftype = "RANGED";
            sword.SetActive(false);

            if (slotExistence[0] != "") { // vagyis ha van az adott slotban valami es nem ures
                DeactivateEveryOtherGun(0);

                // highlighting item-slot
                HighlightSlot(0); // based on index

                // activating the corresponding gun according to the item slots
                for (int i = 0; i < weapons.Length; i++)
                {
                    if (weapons[i].name == slotExistence[0]) {
                        weapons[i].SetActive(true);   

                        // retrieving active gun index 
                        activeStatusGunIndex = i+1;
                        break;
                    }
                }  

                // modifying bullet counter text on the UI to the corresponding status
                bulletCounter.text = BulletCounts[activeStatusGunIndex-1].ToString();
            } else StartCoroutine(EquippingPlainError());
        }

        if (Input.GetKeyDown("2"))
        {
            ftype = "RANGED";
            sword.SetActive(false);

            if (slotExistence[1] != "") { // vagyis ha van az adott slotban valami es nem ures
                DeactivateEveryOtherGun(1);

                // highlighting item-slot
                HighlightSlot(1); // based on index

                // activating the corresponding gun according to the item slots
                for (int i = 0; i < weapons.Length; i++)
                {
                    if (weapons[i].name == slotExistence[1]) {
                        weapons[i].SetActive(true);    

                        // retrieving active gun index 
                        activeStatusGunIndex = i+1;
                        break;
                    }
                }  

                // modifying bullet counter text on the UI to the corresponding status
                bulletCounter.text = BulletCounts[activeStatusGunIndex-1].ToString();
            } else StartCoroutine(EquippingPlainError());
        }

        if (Input.GetKeyDown("3"))
        {
            ftype = "RANGED";
            sword.SetActive(false);

            if (slotExistence[2] != "") { // vagyis ha van az adott slotban valami es nem ures
                DeactivateEveryOtherGun(2);

                // highlighting item-slot
                HighlightSlot(2); // based on index

                // activating the corresponding gun according to the item slots
                for (int i = 0; i < weapons.Length; i++)
                {
                    if (weapons[i].name == slotExistence[2]) {
                        weapons[i].SetActive(true);   

                        // retrieving active gun index 
                        activeStatusGunIndex = i+1;
                        break; 
                    }
                }  

                // modifying bullet counter text on the UI to the corresponding status
                bulletCounter.text = BulletCounts[activeStatusGunIndex-1].ToString();
            } else StartCoroutine(EquippingPlainError());
        }

        if (Input.GetKeyDown("4"))
        {
            ftype = "RANGED";
            sword.SetActive(false);

            if (slotExistence[3] != "") { // vagyis ha van az adott slotban valami es nem ures
                DeactivateEveryOtherGun(3);

                // highlighting item-slot
                HighlightSlot(3); // based on index

                // activating the corresponding gun according to the item slots
                for (int i = 0; i < weapons.Length; i++)
                {
                    if (weapons[i].name == slotExistence[3]) {
                        weapons[i].SetActive(true); 

                        // retrieving active gun index 
                        activeStatusGunIndex = i+1;
                        break;   
                    }
                }  

                // modifying bullet counter text on the UI to the corresponding status
                bulletCounter.text = BulletCounts[activeStatusGunIndex-1].ToString();
            } else StartCoroutine(EquippingPlainError()); 
        }

        if (Input.GetKeyDown("5"))
        {
            ftype = "RANGED";
            sword.SetActive(false);

            if (slotExistence[4] != "") { // vagyis ha van az adott slotban valami es nem ures
                DeactivateEveryOtherGun(4);

                // highlighting item-slot
                HighlightSlot(4); // based on index

                // activating the corresponding gun according to the item slots
                for (int i = 0; i < weapons.Length; i++)
                {
                    if (weapons[i].name == slotExistence[4]) {
                        weapons[i].SetActive(true); 

                        // retrieving active gun index 
                        activeStatusGunIndex = i+1;
                        break;   
                    }
                }

                // modifying bullet counter text on the UI to the corresponding status
                bulletCounter.text = BulletCounts[activeStatusGunIndex-1].ToString(); // -1 mert tombben dolgozunk, ami 0-rol indul, a fegyverek meg 1-rol
            } else StartCoroutine(EquippingPlainError()); 
        }
        

    }

    public void DeactivateEveryOtherGun(int currentGunSlotIndex)
    {
        // ez a fegyver neve amin kivul mindent deaktivalni kell
        string currentGunName = slotExistence[currentGunSlotIndex];

        for (int i = 0; i < usableGuns.Count; i++)
        {
            // ha nem a current fegyver nevet nezzuk a rendelkezesre allo fegyverek kozul, akkor deaktivaljuk
            if (currentGunName != usableGuns[i].name)
                usableGuns[i].SetActive(false);
        }
    }

    public void DeactivateAllGuns()
    {
        for (int i = 0; i < usableGuns.Count; i++)
            usableGuns[i].SetActive(false);
    }

    public void AddToInventory(int index)
    {

        slotsUsed += 1;

        Debug.Log("This index added to inventory:" + index);

        // elso lehetseges ures helyre berakas az existence array-ben
        for (int i = 0; i < slotExistence.Length; i++)
        {
            if (slotExistence[i] == "") { // vagyis ha nincs benne semmi
                slotExistence[i] = "gun" + index.ToString();

                // uj fegyver behelyezese a UI-ban, a megfelelo helyre
                // a UI-k 0-tol szamozva vannak hasznalva
                guns[index-1].transform.position = slots[i].transform.position; // azert kell a -1, mert a fegyverek 1-rol indulnak
                break;

                // to correct -->
            } 
        }

        // Az adott indexu fegyver hozzaadasa a hasznalhato fegyverek listajaba
        usableGuns.Add(weapons[index-1]); // we have to store them in 'weapons' array, bcs we cant find gameobject with thag while it is inactive      
        
    }

    public void RemoveFromUsable(int index)
    {
        // eliberating a free space
        slotsUsed -= 1;

        // removing gun from usable gun list
        for (int i = 0; i < usableGuns.Count; i++)
        {
            if (usableGuns[i].name == ("gun" + index.ToString()))
            {
                usableGuns[i].SetActive(false);
                usableGuns.RemoveAt(i);
                break; // kell a break hogy biztosan ne fusson tul a ciklus
            }
        }
    }

    IEnumerator EquippingPlainError()
    {
        SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.errorSound);
        // displaying error message with red font color
        textHolder.GetComponent<Text>().color = Color.red;
        if (language == "EN" || language == "")
            textHolder.GetComponent<Text>().text = "You can't equip an unused slot!";

        if (language == "HU")
            textHolder.GetComponent<Text>().text = "Üres slot-ot nem tudsz használni!";

        yield return new WaitForSeconds(3f);

        textHolder.GetComponent<Text>().text = ""; 
        textHolder.GetComponent<Text>().color = Color.white;
    }

    void HighlightSlot(int index)
    {
        highlight.transform.position = slots[index].transform.position;
    }

    public void ConsumeBullets()
    {
        int activeGunIndex = activeStatusGunIndex;
        switch (activeGunIndex) {
            case 1:
                BulletCounts[0]--;
                bulletCounter.text = BulletCounts[0].ToString();
                break;
            case 2:
                BulletCounts[1]--;
                bulletCounter.text = BulletCounts[1].ToString();
                break;
            case 3:
                BulletCounts[2]--;
                bulletCounter.text = BulletCounts[2].ToString();
                break;
            case 4:
                BulletCounts[3]--;
                bulletCounter.text = BulletCounts[3].ToString();
                break;
            case 5:
                BulletCounts[4]--;
                bulletCounter.text = BulletCounts[4].ToString();
                break;
            case 6:
                BulletCounts[5]--;
                bulletCounter.text = BulletCounts[5].ToString();
                break;
            case 7:
                BulletCounts[6]--;
                bulletCounter.text = BulletCounts[6].ToString();
                break;
            case 8:
                BulletCounts[7]--;
                bulletCounter.text = BulletCounts[7].ToString();
                break;
        }
    }

}
