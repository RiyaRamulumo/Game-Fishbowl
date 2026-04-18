using UnityEngine;

public class swing : MonoBehaviour
{
    public float speed = 2f;
    public float angle = 15f;

    public float swingTime = 5f;
    public float stopTime = 5f;

    private float timer = 0f;
    private bool isSwinging = true;

    void Update()
    {
        timer += Time.deltaTime;

        if (isSwinging)
        {
            // Swing movement
            float rotation = Mathf.Sin(Time.time * speed) * angle;
            transform.rotation = Quaternion.Euler(0, 0, rotation);

            // After 5 seconds → stop
            if (timer >= swingTime)
            {
                isSwinging = false;
                timer = 0f;
            }
        }
        else
        {
            // Stop movement (freeze rotation)
            if (timer >= stopTime)
            {
                isSwinging = true;
                timer = 0f;
            }
        }
    }
}
