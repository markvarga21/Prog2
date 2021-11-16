using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossCollision : MonoBehaviour
{
    // referencing boss animator
    public Animator bossAnimator;

    // extracting player's current damage
    PlayerStats playerStats;

    // boss stats
    int bossHP = 300;

    public GameObject bossDieEffect;

    // damaging effects
    public GameObject damageText;
    TextMeshPro textMesh;

    public GameObject finishFlag;

    public Slider bossHPSlider;

    void Start()
    {
        // initializing slider to max and filling it up with max hp
        bossHPSlider.maxValue = bossHP;
        bossHPSlider.value = bossHP;

        // referencing playerstats script for extracting player's damage
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            BossTakeDamage(playerStats.PlayerDamage);
            StartCoroutine(PlayHitEffect());
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Sword")
        {
            BossTakeDamage(playerStats.PlayerDamage);
            StartCoroutine(PlayHitEffect());
        }
    }

    IEnumerator PlayHitEffect()
    {
        bossAnimator.SetBool("bossIsHit", true);
        yield return new WaitForSeconds(0.3f);
        bossAnimator.SetBool("bossIsHit", false);
    }

    public void BossTakeDamage(int damage)
    {
        GameObject damageEffect = Instantiate(damageText, this.transform.position + new Vector3(9.6f, -2.3f, 0), Quaternion.identity);

        // referencing textmesh pro component
        textMesh = damageEffect.GetComponent<TextMeshPro>();

        // if the damage is greater than 20, turn text into red
        if (damage > 20)  {
            textMesh.color = new Color(255, 0, 0, 255);
            textMesh.fontSize = 5;
        }

        // setting text
        textMesh.SetText(damage.ToString());

        // playing 'animation'
        StartCoroutine(DamageTextAnimation(damageEffect)); // +0.4ig menjen fel az Y-on

        // destroying text object
        Destroy(damageEffect, 0.5f);


        if (bossHP - damage > 0) {
            bossHP -= damage;
            bossHPSlider.value = bossHP;
        }
        else { 
            bossHPSlider.value = 0;
            BossDie(); 
        }
    }

    void BossDie()
    {
        // playing the animation
        bossAnimator.SetBool("bossIsDead", true);

        // destroying the boss 
        Destroy(this.gameObject, 1.5f);

        // playing die effect
        GameObject effect = Instantiate(bossDieEffect, this.transform.position, Quaternion.identity);
        Destroy(effect, 0.5f);
        Destroy(this.gameObject);


        // enabling finish flag
        finishFlag.SetActive(true);
    }

    IEnumerator DamageTextAnimation(GameObject txt)
    {
        float threshold = 0f;
        while (threshold <= 0.05f)
        {
            threshold += 0.01f;
            txt.transform.position += new Vector3(0, threshold, 0);
            yield return new WaitForSeconds(0.01f);
        } 
    }
}
