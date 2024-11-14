using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public int maxJumps = 2;
    public bool grounded;

    private float moveInput;
    private Rigidbody2D rb;
    private Animator anim;
    private int jumpCount;
    private LadderMovement ladderMovement;

    public bool IsJumping { get; private set; } 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ladderMovement = GetComponent<LadderMovement>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        UpdateAnimations();
    }

    private void HandleMovement()
    {
        moveInput = UserInput.instance.moveInput.x;
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        FlipSprite(moveInput);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (grounded || jumpCount < maxJumps))
        {
            // Exit climbing before jumping
            if (ladderMovement != null && ladderMovement.isClimbing)
            {
                ladderMovement.StopClimbing();
            }

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            grounded = false;
            IsJumping = true; // Set jumping state
            anim.SetTrigger("jump");
            jumpCount++;
        }

        if (grounded)
        {
            IsJumping = false;
        }
    }

    private void UpdateAnimations()
    {
        anim.SetBool("run", Mathf.Abs(rb.linearVelocity.x) > 0.01f);
        anim.SetBool("grounded", grounded);
    }

    private void FlipSprite(float moveInput)
    {
        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            jumpCount = 0;
            IsJumping = false;
        }
    }
}
