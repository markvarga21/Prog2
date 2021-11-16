using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using UnityEngine.UI;

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

    public float BulletDestroyTime = 7f;

    // bullet consuming
    InventoryManager invManager;

    // error message text handling
    Text textHolder;

    // language preferences
    LanguageSettings langSet; // KIVENNI A COMMENTET A VEGEN
    string language;

    // Start is called before the first frame update
    void Start()
    {
        invManager = GameObject.FindWithTag("Inventory").GetComponent<InventoryManager>();
        textHolder = GameObject.FindWithTag("WeaponAddedText").GetComponent<Text>();

        // language settings
        langSet = GameObject.FindWithTag("LanguageSettings").GetComponent<LanguageSettings>();
        language = langSet.currentLanguage;
        // KIVENNI A COMMENTET A VEGEN
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - (Vector2)Gun.position;
        FaceMouse();

        if (Input.GetMouseButton(0)) {
            if (invManager.BulletCounts[invManager.activeStatusGunIndex-1] > 0) {
                if (Time.time > ReadyForNextShot) {
                    ReadyForNextShot = Time.time + 1/fireRate;
                    shoot();
                }
            } else StartCoroutine(OutOfBullets()); 
        }

    }

    void FaceMouse()
    {
        Gun.transform.right = direction;
    }

    void shoot()
    {
        // playing shoot sound
        SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.easterEgg);

        // bullet ins
        GameObject BulletIns = Instantiate(Bullet, ShootPoint.position, ShootPoint.rotation);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(BulletIns.transform.right * BulletSpeed);

        // removing a bullet from current active gun
        invManager.ConsumeBullets();

        // playing animation
        gunAnimator.SetTrigger("Shoot");

        // destroying bullet inst.
        //CameraShaker.Instance.ShakeOnce(1.2f, 0.8f, 0.1f, 0.15f);
        Destroy(BulletIns, BulletDestroyTime);
    }

    IEnumerator OutOfBullets()
    {
        SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.errorSound);
        // displaying error message with red font color
        textHolder.GetComponent<Text>().color = Color.red;
        if (language == "EN" || language == "")
            textHolder.GetComponent<Text>().text = "You ran out of bullets!";

        if (language == "HU")
            textHolder.GetComponent<Text>().text = "Nincsen több lövedéked!";

        yield return new WaitForSeconds(3f);

        textHolder.GetComponent<Text>().text = ""; 
        textHolder.GetComponent<Text>().color = Color.white;
    }
}
