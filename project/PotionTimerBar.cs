using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionTimerBar : MonoBehaviour
{

    public Slider slider;
    

    void Start()
    {
        
    }

    public void SetMaxHealth(int health)
    {
        //slider.maxValue = health;
        //slider.value = health;
    }

    public void SetHealth(int health)
    {
        //slider.value = health;
    }

    public void StartCounting(int time)
    {
        StartCoroutine(Countdown(time));
    }

    IEnumerator Countdown(int time) 
    {
        while (time > 0) {
            Debug.Log(time);
            yield return new WaitForSeconds(1f);
            time--;
        }
    }
}
