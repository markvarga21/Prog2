using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{

    public Slider slider;
    public Vector3 offset; // we use this because not all enemies have the same height
    
    int enemyMaxHP = 100;

    public void SetHealth(int health)
    {

        slider.value = health;

    }

    void Start()
    {
        // a legelejen bealltijuk mindkett a maxra
        slider.maxValue = enemyMaxHP;
        slider.value = enemyMaxHP;
    }

    void Update()
    {
        // moving the slider with the enemy
        // 3D-bol konvertal 2D screenpointba
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
