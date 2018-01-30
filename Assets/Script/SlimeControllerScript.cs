using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeControllerScript : MonoBehaviour {

    public float maxSpeed = 5f;
    private bool facingRight = true;

    Animator animator;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    private Rigidbody2D playerRigidbody;

    public float jumpForce = 700f;

    bool doubleJump = false;


    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

       

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        animator.SetBool("Ground", grounded);

        if(grounded)
        {
            doubleJump = false;
        }

        animator.SetFloat("vSpeed", playerRigidbody.velocity.y);

        float move = Input.GetAxis("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(move));



        playerRigidbody.velocity = new Vector2(move * maxSpeed, playerRigidbody.velocity.y);

        if(move > 0 && !facingRight)
        {
            Flip();
        }
        else if(move < 0 && facingRight)
        {
            Flip();
        }
	}

    void Update()
    {
        //Simple jump
        /* if(grounded && Input.GetKeyDown(KeyCode.Space))
         {
             animator.SetBool("Ground", false);
             playerRigidbody.AddForce(new Vector2(0f, jumpForce));
         }*/

        //double jump
        if ((grounded || !doubleJump) && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Ground", false);
            playerRigidbody.AddForce(new Vector2(0f, jumpForce));

            if(!doubleJump && !grounded)
            {
                doubleJump = true;
            }

        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
