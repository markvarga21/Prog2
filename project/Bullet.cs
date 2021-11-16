using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject hitEffect;

    void Start()
    {
    }

    void Update()
    {
        Physics2D.IgnoreLayerCollision(8, 9);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.memeExplosion);
        GameObject effect = Instantiate(hitEffect, this.transform.position, Quaternion.identity); // quaternion.identity == syntax for no rotation
        Destroy(effect, 0.3f);
        Destroy(this.gameObject);
    }
}
