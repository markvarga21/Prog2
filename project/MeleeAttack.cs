using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    
    public GameObject sword;
    Animator swordAnimator;

    InventoryManager invManager;

    void Start()
    {
        swordAnimator = sword.GetComponent<Animator>();
        invManager = GameObject.FindWithTag("Inventory").GetComponent<InventoryManager>();
    }

    
    void Update()
    {
        if (Input.GetMouseButton(0) && invManager.ftype == "MELEE")
            swordAnimator.Play("Base Layer.Sword_leftClickAttack");

        if (Input.GetMouseButton(1) && invManager.ftype == "MELEE")
            swordAnimator.Play("Base Layer.Sword_rightClickAttack");
    }

}
