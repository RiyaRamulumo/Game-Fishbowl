using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 7f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private bool isGrounded;

    [Header("States")]
    private bool canMove = false;
    private bool hasJumped = false;
    private bool inWater = false;
    private bool shelfTutorialShown = false;

    [Header("UI")]
    public GameObject moveText;
    public GameObject jumpText;
    public GameObject waterText;
    public GameObject shelfText;
    public GameObject AvoidPersonText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Stop falling at start
        rb.gravityScale = 0f;

        // Initial UI
        if (moveText != null) moveText.SetActive(true);
        if (jumpText != null) jumpText.SetActive(false);
        if (waterText != null) waterText.SetActive(false);
        if (shelfText != null) shelfText.SetActive(false);
        if (AvoidPersonText != null) AvoidPersonText.SetActive(false);
    }

    void Update()
    {
        // STEP 1: WAIT FOR MOVE INPUT
        if (!canMove)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                canMove = true;
                rb.gravityScale = 2f;

                if (moveText != null) moveText.SetActive(false);
                if (jumpText != null) jumpText.SetActive(true);
            }
            return;
        }

        // MOVEMENT
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // JUMP
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (inWater)
            {
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

    // TRIGGERS (Water, Shelf, Person)
    void OnTriggerEnter2D(Collider2D other)
    {
        // WATER
        if (other.CompareTag("Water") && !shelfTutorialShown)
        {
            inWater = true;
            shelfTutorialShown = true;

            if (waterText != null)
                waterText.SetActive(false);

            if (shelfText != null)
                shelfText.SetActive(true);

            if (AvoidPersonText != null)
                AvoidPersonText.SetActive(true);
        }

        // SHELF
        if (other.CompareTag("Shelf"))
        {
            if (shelfText != null)
                shelfText.SetActive(false);

            if (AvoidPersonText != null)
                AvoidPersonText.SetActive(false);
        }

        // PERSON = DIE
        if (other.CompareTag("Person"))
        {
            Die();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            inWater = false;
        }
    }

    // DEATH FUNCTION
    void Die()
    {
        Debug.Log("Fish died!");

        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;
        canMove = false;

        gameObject.SetActive(false);
    }
}