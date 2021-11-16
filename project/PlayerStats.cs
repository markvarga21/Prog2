using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{

    // stuffs related to his health
    public int maxHP = 100;
    public int currentHP;

    public HealthBar healthBar;

    // stuffs related to his speed
    Character2DController characterController;
    public float playerSpeed;

    // money
    public int playerMoney;
    public int coinWorth = 20;
    public GameObject moneyHeader;

    // checkpoint system
    public float lastCheckpointX = 9999999999f; // hibakezelesre
    public float lastCheckpointY = 9999999999f;
    float respawnOffset = 0.4f;
    GameObject startPos;

    // die message writeout
    GameObject textHolder;

    // language
    LanguageSettings langSet;
    string language; 

    Animator playerAnimator;

    // player damage
    public int PlayerDamage = 20;

    void Start()
    {
        // initializing HP to max (100) at the beginning
        currentHP = maxHP;
        healthBar.SetMaxHealth(maxHP);

        // finding character controller script
        characterController = GameObject.Find("proba player").GetComponent<Character2DController>();
        playerSpeed = characterController.MovementSpeed;

        // initializing money to 0
        playerMoney = 0;
        moneyHeader.GetComponent<Text>().text = "0";

        // die message text holder
        textHolder = GameObject.FindWithTag("WeaponAddedText");
        
        // language settings
        langSet = GameObject.FindWithTag("LanguageSettings").GetComponent<LanguageSettings>();
        language = langSet.currentLanguage;

        // teleporting back the player if he dies but had not reached a checkpoin yet
        startPos = GameObject.FindWithTag("StartPos");
        lastCheckpointX = lastCheckpointY = 9999999999f;

        // referencing player animator
        playerAnimator = GameObject.FindWithTag("Player").GetComponent<Animator>();

        // initializing player damage, because if we do it outside of the Start function it will not take any value (unity stuff)
        PlayerDamage = 20;
    }


    void Update()
    {
        // updating the speed in every second
        characterController.MovementSpeed = playerSpeed;

        if (Input.GetKeyDown("j"))
            Debug.Log("Player money from PlayerStats script: " + playerMoney);

        // azert rakom az updatebe mivel mas scripten keresztul valtozik az erteket
        moneyHeader.GetComponent<Text>().text = playerMoney.ToString();

    }

    public void TakeDamage(int damage)
    {
        if (currentHP - damage > 0) {
            currentHP -= damage;
            healthBar.SetHealth(currentHP);
            StartCoroutine(hurtAnimation());
            
        } else Die();
    }

    public void AddMoneyOnCoinCollision()
    {
        playerMoney += coinWorth;
    }

    public void Die()
    {
        SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.Die);
        // respawning the player on the last checkpoint

        // first case, if we are on the first level
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("Level1")) {
            if (lastCheckpointX != 9999999999f && lastCheckpointY != 9999999999f)
                gameObject.transform.position = new Vector2(lastCheckpointX, lastCheckpointY + respawnOffset);
            else 
                gameObject.transform.position = startPos.transform.position;
        }

        // and second case, if we are on the boss fight, level 2 
        if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Level2"))
        {
            Transform Level2StartPos = GameObject.FindWithTag("StartPos").transform;
            gameObject.transform.position = new Vector2(Level2StartPos.position.x, Level2StartPos.position.y + respawnOffset);
        }

        // setting his HP to max
        currentHP = maxHP;
        healthBar.SetMaxHealth(maxHP);

        // writing out that the player has died
        StartCoroutine(DieMessage()); 

    }

    void OnTriggerEnter2D(Collider2D checkpoint)
    {
        if (checkpoint.gameObject.tag == "checkpoint") {
            SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.checkPointSound);
            //Debug.Log("We found " + checkpoint.gameObject.name + " on position: " + checkpoint.gameObject.transform.position.x + ", " + checkpoint.gameObject.transform.position.y);
            lastCheckpointX = checkpoint.gameObject.transform.position.x;
            lastCheckpointY = checkpoint.gameObject.transform.position.y;
            Debug.Log("Last checkpont: " + lastCheckpointX + ", " + lastCheckpointY);
        }
    }

    IEnumerator DieMessage()
    {
        characterController.enabled = false;
        textHolder.GetComponent<Text>().color = Color.red;
        if (language == "EN" || language == "")
            textHolder.GetComponent<Text>().text = "You died!";

        if (language == "HU")
            textHolder.GetComponent<Text>().text = "Meghaltál!";            

        yield return new WaitForSeconds(3f);

        textHolder.GetComponent<Text>().text = "";
        textHolder.GetComponent<Text>().color = Color.red;
        characterController.enabled = true;
    }

    IEnumerator hurtAnimation()
    {
        playerAnimator.SetBool("isHurt", true);
        yield return new WaitForSeconds(0.5f);
        playerAnimator.SetBool("isHurt", false);
    }

}
