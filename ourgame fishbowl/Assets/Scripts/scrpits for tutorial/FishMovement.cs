using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    private Rigidbody2D rb;
    private bool isGrounded;

    [Header("States")]
    private bool canMove = false;
    private bool hasJumped = false;
    private bool inWater = false;

    [Header("UI")]
    public GameObject moveText;
    public GameObject jumpText;
    public GameObject waterText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Stop falling at start
        rb.gravityScale = 0f;

        // Make sure only move text is active
        if (moveText != null) moveText.SetActive(true);
        if (jumpText != null) jumpText.SetActive(false);
        if (waterText != null) waterText.SetActive(false);
    }

    void Update()
    {
        // STEP 1: WAIT FOR MOVE INPUT
        if (!canMove)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                canMove = true;

                // Enable gravity
                rb.gravityScale = 2f;

                // Switch UI
                if (moveText != null) moveText.SetActive(false);
                if (jumpText != null) jumpText.SetActive(true);
            }

            return;
        }

        // MOVEMENT
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // JUMP (GROUND OR WATER)
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (inWater)
            {
                // Softer jump in water
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * 1.0f);
            }
            else if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }

            // First jump → switch tutorial
            if (!hasJumped)
            {
                hasJumped = true;

                if (jumpText != null) jumpText.SetActive(false);
                if (waterText != null) waterText.SetActive(true);
            }
        }
    }

    // GROUND CHECK
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }

    // WATER DETECTION
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            inWater = true;

            // Hide water tutorial when player reaches water
            if (waterText != null)
                waterText.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            inWater = false;
        }
    }
}