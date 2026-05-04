using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    public Vector3 offset;

    // NEW: camera limits
    public float minX, maxX;
    public float minY, maxY;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;

        // Clamp position (LIMIT CAMERA)
        float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
        float clampedY = transform.position.y;

        Vector3 finalPosition = new Vector3(clampedX, clampedY, transform.position.z);

        // Smooth movement
        transform.position = Vector3.Lerp(transform.position, finalPosition, smoothSpeed * Time.deltaTime);
    }
}