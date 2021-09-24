using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2DController : MonoBehaviour
{


    public bool FacingRight;

    public float MovementSpeed = 1;
    public float JumpForce = 1;

    public Animator animator;

    public Rigidbody2D PlayerRigidbody;

    public GameObject Guns;

    [SerializeField] Transform groundCheckCollider; // serialized field, thus we can reference it from the inspector
    [SerializeField] bool isGrounded = false;
    const float groundCheckRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        PlayerRigidbody = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        GroundCheck();

        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

        animator.SetFloat("Speed", Mathf.Abs(movement));

        if (Input.GetKeyDown("w")) {
            PlayerRigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            animator.SetBool("IsJumping", true);
        }

        // Flipping
        if ((movement < 0 && FacingRight) || (movement > 0 && !FacingRight))
        {
            FacingRight = !FacingRight;
            transform.Rotate(new Vector3(0, 180, 0));
            Guns.transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    void GroundCheck()
    {
        isGrounded = false;
        // Check if the GroundCheck object is colliding with other 
        // 2D Colliders that are in the 'Ground' layer
        // If yes (isGrounded true) else (isGrounded false)
        // spawns a circle to that empty and checks if its colliding with something or not
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
            isGrounded = true;

        // As long as we are grounded the "Jump" bool in the animator is disabled
        animator.SetBool("IsJumping", !isGrounded);

    }

    void OnLevelWasLoaded(int level)
    {
        FindStartPos();
    }

    void FindStartPos()
    {
        transform.position = GameObject.FindWithTag("StartPos").transform.position;
    }
}
