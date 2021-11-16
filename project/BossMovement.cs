using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossMovement : MonoBehaviour
{
    
    // enum to store whether the player is on it's left side or the other
    enum Side { LEFT, RIGHT, DEFAULT }


    public Transform leftBound;
    public Transform rightBound;

    Transform player;

    Side playerSide;

    float bossSpeed = 1f;

    Animator bossAnimator;

    // hogy ne collidoljunk a playerrel 
    float offset = 0.3f;

    int szorzo = 0;

    void Start()
    {
        playerSide = Side.DEFAULT;
        bossAnimator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
    }

    
    void Update()
    {
        if (BossIsArroundPlayer(player, this.transform)) {
            // playing boss attack animation
            bossAnimator.SetBool("isAttacking", true);
            bossAnimator.SetBool("isMoving", false);
            // damage player
        } else {
            bossAnimator.SetBool("isAttacking", false);
            bossAnimator.SetBool("isMoving", true);
        } 


        if (InIntervalOf(leftBound.position.x, this.transform.position.x, player))
            playerSide = Side.LEFT;
        else 
            playerSide = Side.RIGHT;

        if (playerSide == Side.LEFT)  {
            transform.localScale = new Vector3(-2, 2, 2);
            szorzo = 1;
        }
        if (playerSide == Side.RIGHT)  {
            transform.localScale = new Vector3(2, 2, 2);
            szorzo = -1;
        }

        transform.position = Vector3.MoveTowards(
            transform.position, 
            new Vector3(player.position.x + (szorzo * offset), transform.position.y, transform.position.z), 
            bossSpeed * Time.deltaTime);
    }

    bool InIntervalOf(float a, float b, Transform target)
    {
        float x = target.position.x;
        if (a <= x && b > x) return true;
        return false;
    }

    bool BossIsArroundPlayer(Transform player, Transform boss)
    {
        float absXdist = Math.Abs(player.position.x - boss.position.x);
        float absYdist = Math.Abs(player.position.y - boss.position.y);
        if (absXdist <= 1f && absYdist <= 1f) return true;
        return false;
    }

}
