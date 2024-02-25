using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// 
/// 
/// - A regler :
/// 
///
/// 
/// 
/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// </summary>


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float baseGravityScale;
    public float wallJumpCD;

    private bool isJumping;
    public bool wantsJumping;
    public bool isGrounded;
    public bool isOnRightWall;
    private bool isJumpingRIGHT;
    public bool isOnLeftWall;
    private bool isJumpingLEFT;
    private float timerJump = 0f;
    private bool jumpedRIGHT = false;
    private bool jumpedLEFT = false;
    private bool canJumpRIGHT = true;
    private bool canJumpLEFT = true;
    public bool canClimb = false;
    public bool hasClimbPower = false;

    public float vertical;

    public bool dead = false;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;

    public Transform wallCheckLeftUp;
    public Transform wallCheckLeftDown;
    public Transform wallCheckRightUp;
    public Transform wallCheckRightDown;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Vector3 velocity = Vector3.zero;

    private void Update()
    {

        vertical = Input.GetAxis("Vertical");

// Check jump
        if (Input.GetButtonDown("Jump") && ( isGrounded || isOnLeftWall || isOnRightWall ) && !dead)
        {
            wantsJumping = true;
        }

        if (jumpedRIGHT)
        {
            timerJump += Time.deltaTime;
            canJumpRIGHT = false;

            if (timerJump > wallJumpCD) 
            {
                jumpedRIGHT = false;
                canJumpRIGHT = true;
                timerJump = 0f;
            }
        }

        if (jumpedLEFT)
        {
            timerJump += Time.deltaTime;
            canJumpLEFT = false;

            if (timerJump > wallJumpCD)
            {
                jumpedLEFT = false;
                canJumpLEFT = true;
                timerJump = 0f;
            }
        }
    }
    void FixedUpdate()
    {
// Check if grounded
        if (!canClimb)
        {
            isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        }

// Check if touching wall
        isOnLeftWall = Physics2D.OverlapArea(wallCheckLeftUp.position, wallCheckLeftDown.position);
        isOnRightWall = Physics2D.OverlapArea(wallCheckRightUp.position, wallCheckRightDown.position);

// Wall climb

        Vector2 zeroY = new Vector2(rb.velocity.x, 0);

        if (canClimb)
        {
            rb.velocity = zeroY;
            Vector2 dir = new Vector2(0f, (Input.GetAxis("Vertical")) / 10 );
            transform.Translate(dir);

        }

// Variable horinzontal movement
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

// Wants to jump
        if (wantsJumping && isGrounded)
        {
            isJumping = true;
            wantsJumping = false;
        }

// Wants to wall jump RIGHT
        if (wantsJumping && isOnRightWall && canJumpRIGHT)
        {
            isJumpingRIGHT = true;
            wantsJumping = false;
        }

// Wants to wall jump LEFT
        if (wantsJumping && isOnLeftWall && canJumpLEFT)
        {
            isJumpingLEFT = true;
            wantsJumping = false;
        }

        MovePlayer(horizontalMovement);

// Flip player
        Flip(rb.velocity.x);

// Velocity variable for animator
        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);

    }

// Move player function
    void MovePlayer(float _horizontalMovement)
    {
        
        if (!dead) 
        { 
            Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
        }
        

        if (isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
        if (isJumpingRIGHT && canJumpRIGHT)
        {
            rb.AddForce(new Vector2(-jumpForce, jumpForce));
            isJumpingRIGHT = false;
            jumpedRIGHT = true;
        }
        if (isJumpingLEFT && canJumpLEFT)
        {
            rb.AddForce(new Vector2(jumpForce, jumpForce));
            isJumpingLEFT = false;
            jumpedLEFT = true;
        }

    }

// Flip player function
    void Flip(float _velocity) 
    { 
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    public void Die()
    {
        dead = true;
        rb.velocity = Vector2.zero;
    }

    public void Live() 
    { 
        dead = false; 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Climb") && hasClimbPower )
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            canClimb = true;
        }

        if (collision.transform.CompareTag("Collectible"))
        {
            hasClimbPower = true;
            Destroy(collision.transform.parent.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Climb") && hasClimbPower)
        {
            rb.gravityScale = baseGravityScale;
            canClimb = false;
        }
    }
}
