using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    public float vertical;
    public float speed = 8f;
    public bool isLadder;
    private bool isClimbing;

    public float defaultGravityScale = 3f;

    [SerializeField] private Rigidbody2D rb;

    // adding climbing animation
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // referencing animator
        animator = GameObject.FindWithTag("Player").GetComponent<Animator>();
    }

    void Update()
    {
        vertical = Input.GetAxis("Vertical"); // this returns a -1 or a +1

        // ha letra van es felfele mennek akkor az isClimbing igaz 
        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
    }

    // mivel physics-et manipulalunk, ezert az exact-abb szamolasok erdekeben, FixedUpdate-t hasznalunk
    void FixedUpdate()
    {
        if (isClimbing)
        {
            animator.SetBool("isClimbing", true);
            rb.gravityScale = 0f;

            // az x erteke valtozatlan, az y viszont valtozik, felfele vagy lefele
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        } else {
            rb.gravityScale = defaultGravityScale;
            animator.SetBool("isClimbing", false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }
}
