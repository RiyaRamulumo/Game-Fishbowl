using UnityEngine;
using TMPro;

public class TimerForFish : MonoBehaviour
{
    public float maxTime = 5f;
    private float timer;

    private bool inWater = false;
    private SpriteRenderer sr;

    public TextMeshProUGUI timerText; // UI text

    void Start()
    {
        timer = maxTime;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!inWater)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                Die();
            }
        }
        else
        {
            timer = maxTime;
        }

        // Update the text on screen
        timerText.text = "Time Left: " + timer.ToString("F1");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            inWater = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            inWater = false;
        }
    }

    void Die()
    {
        Debug.Log("Fish died!");
        sr.enabled = false;
    }
}
