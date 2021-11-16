using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EnemyStats : MonoBehaviour
{
    
    public int enemyHP = 100;
    public int enemyDamage = 15;

    public EnemyHealthBar enemyHealthBar;

    // enemy die effect
    public GameObject enemyDieEffect;
    public GameObject damageText;
    TextMeshPro textMesh;

    // player damage
    PlayerStats playerStats;

    // melee attack damage
    MeleeAttack meleeAttack;

    void Start()
    {
        enemyHP = 100;
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        meleeAttack = GameObject.FindWithTag("Player").GetComponent<MeleeAttack>();
    }

    
    void Update()
    {
       
    }

    public void EnemyTakeDamage(int dmg)
    {
        GameObject damageEffect = Instantiate(damageText, this.transform.position + new Vector3(9.6f, -2.3f, 0), Quaternion.identity);

        // referencing textmesh pro component
        textMesh = damageEffect.GetComponent<TextMeshPro>();

        // if the damage is greater than 20, turn text into red
        if (dmg > 20)  {
            textMesh.color = new Color(255, 0, 0, 255);
            textMesh.fontSize = 5;
        }

        // setting text
        textMesh.SetText(dmg.ToString());

        // playing 'animation'
        StartCoroutine(DamageTextAnimation(damageEffect)); // +0.4ig menjen fel az Y-on

        // destroying text object
        Destroy(damageEffect, 0.5f);

        if (enemyHP - dmg > 0) {
            enemyHP -= dmg;
            enemyHealthBar.SetHealth(enemyHP);
        }
        else {
            EnemyDie();
        }
    }

    void EnemyDie()
    {
        GameObject effect = Instantiate(enemyDieEffect, this.transform.position, Quaternion.identity);
        Destroy(effect, 0.5f);
        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet") {
            Debug.Log("We damaged enemy with " + playerStats.PlayerDamage + " where player's damage is = " + playerStats.PlayerDamage);
            EnemyTakeDamage(playerStats.PlayerDamage);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Sword")
        {
            EnemyTakeDamage(playerStats.PlayerDamage);
        }
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
