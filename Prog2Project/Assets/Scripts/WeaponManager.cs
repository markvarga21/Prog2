using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    int totalWeapons = 1;
    public int currentWeaponIndex;

    public int maxLen = 3;

    public GameObject[] guns;
    public GameObject weaponHolder;
    public GameObject currentGun;
   
    void Start()
    {
       totalWeapons = weaponHolder.transform.childCount;
       guns = new GameObject[totalWeapons];

       for (int i = 0; i < totalWeapons; i++)
       {
        guns[i] = weaponHolder.transform.GetChild(i).gameObject;
        guns[i].SetActive(false);
       } 

       guns[0].SetActive(true);
       currentGun = guns[0];
       currentWeaponIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0f )
        {
            // next weapon
            if (currentWeaponIndex < totalWeapons-1 && currentWeaponIndex < maxLen-1)
            {
                guns[currentWeaponIndex].SetActive(false);
                currentWeaponIndex += 1;
                guns[currentWeaponIndex].SetActive(true);
                currentGun = guns[currentWeaponIndex];
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f )
        {
            // previsious weapon
            if (currentWeaponIndex > 0)
            {
                guns[currentWeaponIndex].SetActive(false);
                currentWeaponIndex -= 1;
                guns[currentWeaponIndex].SetActive(true);
                currentGun = guns[currentWeaponIndex];
            }
        }
    }
}
