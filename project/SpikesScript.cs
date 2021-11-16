using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesScript : MonoBehaviour
{
    PlayerStats playerstats;

    void Start()
    {
        playerstats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();    
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "spike") // vagyis ha spike-ra lepett/collideolt a player akkor meghal
        {
            playerstats.Die();
        }
    }
}
