using UnityEngine;

public class TrunkMovement : MonoBehaviour
{
    public float moveSpeed = 2f;      // How fast it moves forward
    public float moveDistance = 2f;   // How far it moves

    private Vector3 startPosition;    // Where it starts
    private Vector3 targetPosition;   // Where it should move to

    private bool playerNearby = false;
    private bool hasMoved = false;    // Prevents moving again

    void Start()
    {
        startPosition = transform.position;

        // Move forward (to the right in 2D)
        targetPosition = startPosition + new Vector3(moveDistance, 0, 0);
    }

    void Update()
    {
        // Only move if fish is near AND it hasn't finished moving
        if (playerNearby && !hasMoved)
        {
            // Smoothly move toward target position
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            // Check if it reached the target
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                hasMoved = true; // Stop moving forever
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true; // Start moving
        }
    }
}
