using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ChestManager : MonoBehaviour
{
    public Animator animator;
    GameObject player;

    // lada fegyver
    public GameObject gun;

    // ennyi ideig tart ladanyito animacio
    float gunDisplayTime = 1.5f;

    bool canOpen;
    bool alreadyOpened;


    void Start()
    {
        canOpen = false;
        alreadyOpened = false;
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        // ha ott van a player es meg nem volt egyszer sem kinyitva check
        if (canOpen && !alreadyOpened)
        {
            StartCoroutine(DisplayWeapon());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.ToString() == "Player") {
            Debug.Log("we collided with player");
            canOpen = true;
        }
    }

    IEnumerator DisplayWeapon()
    {   
        animator.SetBool("CollidedWithPlayer", true);
        yield return new WaitForSeconds(gunDisplayTime);
        gun.SetActive(true);
        alreadyOpened = true;
    }


}
