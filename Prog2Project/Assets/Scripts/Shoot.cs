using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Shoot : MonoBehaviour
{

    public Transform Gun;

    public Animator gunAnimator;

    Vector2 direction;

    public GameObject Bullet;

    public float BulletSpeed;

    public Transform ShootPoint;

    public float fireRate;
    float ReadyForNextShot;

    public float BulletDestroyTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - (Vector2)Gun.position;
        FaceMouse();

        if (Input.GetMouseButton(0)) {
            if (Time.time > ReadyForNextShot) {
                ReadyForNextShot = Time.time + 1/fireRate;
                shoot();
            }
        }
    }

    void FaceMouse()
    {
        Gun.transform.right = direction;
    }

    void shoot()
    {
        GameObject BulletIns = Instantiate(Bullet, ShootPoint.position, ShootPoint.rotation);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(BulletIns.transform.right * BulletSpeed);
        gunAnimator.SetTrigger("Shoot");
        CameraShaker.Instance.ShakeOnce(1.2f, 0.8f, 0.1f, 0.15f);
        Destroy(BulletIns, BulletDestroyTime);
    }
}
