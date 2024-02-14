using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float baseGravityScale;

    private bool isJumping;
    private bool wantsJumping;
    public bool isGrounded;
    public bool isOnRightWall;
    private bool isJumpingRIGHT;
    public bool isOnLeftWall;
    private bool isJumpingLEFT;

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
// Check jump
        if (Input.GetButtonDown("Jump"))
        {
            wantsJumping = true;
        }
    }
    void FixedUpdate()
    {
// Check if grounded
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);

// Check if touching wall
        isOnLeftWall = Physics2D.OverlapArea(wallCheckLeftUp.position, wallCheckLeftDown.position);
        isOnRightWall = Physics2D.OverlapArea(wallCheckRightUp.position, wallCheckRightDown.position);

// Variable horinzontal movement
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

// Wants to jump
        if (wantsJumping && isGrounded)
        {
            isJumping = true;
            wantsJumping = false;
        }

// Wants to wall jump RIGHT
        if (wantsJumping && isOnRightWall)
        {
            isJumpingRIGHT = true;
            wantsJumping = false;
        }

// Wants to wall jump LEFT
        if (wantsJumping && isOnLeftWall)
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
        if (!isOnRightWall && !isOnLeftWall || isGrounded)
        {
            Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
        }

        if (isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
        if (isJumpingRIGHT)
        {
            rb.AddForce(new Vector2(-jumpForce, jumpForce));
            isJumpingRIGHT = false;
        }
        if (isJumpingLEFT)
        {
            rb.AddForce(new Vector2(jumpForce, jumpForce));
            isJumpingLEFT = false;
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
 }
