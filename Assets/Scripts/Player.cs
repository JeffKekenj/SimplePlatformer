using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    Rigidbody2D rb2d;
    Animator myAnimator;

    [SerializeField]
    private float movementSpeed;

    private bool facingRight = true;

    //Jumping Variables
    private bool grounded = false;
    private float groundCheckRadius = 0.2f;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private float jumpForce;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
	}

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        //fix this code
        if (grounded && Input.GetAxis("Jump") > 0)
        {
            grounded = false;
            myAnimator.SetBool("isGrounded", grounded);
            rb2d.AddForce(new Vector2(0, jumpForce));
        }
    }
	
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        HandleMovement(horizontal);
        ChangeDirection(horizontal);
        CheckJump();
    }

    void CheckJump()
    {
        //check if we are grounded, if no, then we are falling
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        myAnimator.SetBool("isGrounded", grounded);
        myAnimator.SetFloat("verticalSpeed", rb2d.velocity.y);
    }

    void HandleMovement(float horizontal)
    {
        rb2d.velocity = new Vector2(horizontal * movementSpeed, rb2d.velocity.y);

        myAnimator.SetFloat("speed", Mathf.Abs(rb2d.velocity.x));
    }

    void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    void ChangeDirection(float horizontal)
    {
        if (facingRight && horizontal < 0 || !facingRight && horizontal > 0) {         
            facingRight = !facingRight;
            Flip();
        }
    }
}
