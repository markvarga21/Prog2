using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    
    public Transform waypoint1;
    public Transform waypoint2;
    
    bool collidedWithLeft = false;
    bool collidedWithRight = true;

    int iranyFordito = -1;

    int enemyMovementSpeed = 1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (iranyFordito == -1) {
            Move(iranyFordito);
        }
        if (iranyFordito == 1) {
            Move(iranyFordito);
        }
    }

    void Move(int f)
    {
        this.transform.position += new Vector3(f, 0, 0) * Time.deltaTime * enemyMovementSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "WP1_lvl1")
        {
            iranyFordito *= -1;
            // azert kerul ide a rotate, mivel ez csak egyszer hivodik meg utkozesenkent, s igy nem fog bugolni, hogy oda vissza rotatelodik
            transform.Rotate(new Vector3(0, 180, 0));
        }
        if (other.gameObject.name == "WP2_lvl1")
        {
            iranyFordito *= -1;
            transform.Rotate(new Vector3(0, 180, 0));
        }

    }

}
