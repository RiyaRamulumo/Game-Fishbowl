using UnityEngine;
using TMPro;


public class TimerForFish : MonoBehaviour

{
    public float maxTime = 10f;
private float timer;

private bool inWater = false;
private bool isDead = false;

private Rigidbody2D rb;
private SpriteRenderer sr;

    public TextMeshProUGUI timerText;
public GameObject GameOverText;
public GameObject dropPrefab;

    void Start()
{
    timer = maxTime;
    rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

void Update()
{
    if (isDead) return;

    if (!inWater)
    {
        timer -= Time.deltaTime;
    }
    else
    {
        timer = maxTime;
    }

    // Update timer display
    timerText.text = "Time: " + Mathf.Ceil(timer).ToString();

    if (timer <= 0)
    {
        Die();
    }
}

void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Water"))
    {
        inWater = true;
    }

    if (other.CompareTag("Trap")) // fish dies immediaely when it touches the trap 
    {
        Die();
    }
}

void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            inWater = false;

            // create droplets
            for (int i = 0; i < 5; i++)
            {
                Vector3 offset = new Vector3(
                    Random.Range(-0.3f, 0.3f),
                    Random.Range(0f, 0.3f),
                    0
                );

                GameObject drop = Instantiate(dropPrefab, transform.position + offset, Quaternion.identity);

                Rigidbody2D rbDrop = drop.GetComponent<Rigidbody2D>();

                if (rbDrop != null)
                {
                    rbDrop.AddForce(
                        new Vector2(Random.Range(-1f, 1f), Random.Range(2f, 4f)),
                        ForceMode2D.Impulse
                    );
                }
            }
        }
    }

    void Die()

    {
        isDead = true;

        Debug.Log("Fish Died!");

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }

        //  Change color to red
        if (sr != null)
        {
            sr.color = Color.red;
        }

        //  Flip upside down
        transform.rotation = Quaternion.Euler(0, 0, 180);

        // Show GAME OVER text
        if (GameOverText != null)
        {
            GameOverText.SetActive(true);
        }

        //  Wait before disappearing
        Invoke("HideFish", 1.5f);
    }
    void HideFish()
    {
        gameObject.SetActive(false);
    }
}
