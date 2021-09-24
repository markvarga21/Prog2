using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public int maxHP = 100;
    public int currentHP;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        healthBar.SetMaxHealth(maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    void TakeDamage(int damage)
    {
        currentHP -= damage;
        healthBar.SetHealth(currentHP);
    }

    
}
