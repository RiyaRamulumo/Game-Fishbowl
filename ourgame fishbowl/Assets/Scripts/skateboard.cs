using UnityEngine;

public class skateboard : MonoBehaviour
{
    public float flipForce = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            // Apply torque to make it flip
            Vector3 hitDirection = collision.contacts[0].point - transform.position;

            rb.AddTorque(new Vector3(0, 0, hitDirection.x * flipForce), ForceMode.Impulse);
        }
    }
}