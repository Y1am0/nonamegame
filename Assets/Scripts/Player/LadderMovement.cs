using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private float vertical;
    public bool isLadder;
    public bool isClimbing;
    private float defaultGravityScale;

    public float climbSpeedMultiplier = 0.5f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerMovement playerMovement;

    void Start()
    {
        defaultGravityScale = rb.gravityScale;

        if (!playerMovement)
        {
            playerMovement = GetComponent<PlayerMovement>();
        }
    }

    void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if (isLadder && !playerMovement.IsJumping)
        {
            // Start climbing if vertical input is pressed or already climbing
            if (Mathf.Abs(vertical) > 0f || isClimbing)
            {
                StartClimbing();
            }
        }
        else
        {
            StopClimbing();
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f; // Disable gravity while climbing
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, vertical * playerMovement.moveSpeed * climbSpeedMultiplier);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            StopClimbing();
        }
    }

    public void StartClimbing()
    {
        isClimbing = true;
        rb.gravityScale = 0f; // Disable gravity while climbing
    }

    public void StopClimbing()
    {
        isClimbing = false;
        rb.gravityScale = defaultGravityScale; // Reset gravity
    }
}
