using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChestWeaponPickup : MonoBehaviour
{
    
    InventoryManager invMananager;
    public SpriteRenderer gunSprite;
    public Collider2D gunCollider;

    void Start()
    {
        invMananager = GameObject.Find("Inventory").GetComponent<InventoryManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (invMananager.slotsUsed < 5)
            {
                string gunName = this.gameObject.name.ToString();

                // extracting gun index out of it
                string gunIndexString = gunName.Substring(3);
                int gunIndex = Int32.Parse(gunIndexString); // mivel a gun nevet fogjuk hasznalni a tovabbiakban

                invMananager.AddToInventory(gunIndex);
                gunSprite.enabled = false; // ladasnal javitani kell, mivel a destroy miatt nem lehet felvenni a fegyvert
                gunCollider.enabled = false;
            }
        }
    }
}
