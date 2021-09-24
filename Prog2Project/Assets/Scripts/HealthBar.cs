using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    

    void Start()
    {
        GameObject healthBar = GameObject.FindWithTag("HealthBar");
        DontDestroyOnLoad(healthBar);
    }

    //void Awake()
   // {
     //   DontDestroyOnLoad(this.GameObject);
   // }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
