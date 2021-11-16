using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFlipper : MonoBehaviour
{

    SpriteRenderer spriteRender;
    void Start()
    {
       spriteRender = this.GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        Flip();

    }

    void Flip()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //this.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        if (rotZ < 89 && rotZ > -89)
        {
            spriteRender.flipY = false;
        } else {
            spriteRender.flipY = true;
        }
    }
}
