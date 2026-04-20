using TMPro;
using UnityEngine;

public class YouWon : MonoBehaviour
{
    public GameObject youwon;
    public GameObject disapear;
    public GameObject exitbutton;
    public GameObject pausemenu;
    public Movement movement;
    public Watermeter watermeter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            youwon.SetActive(true);
            pausemenu.SetActive(true);
            

            disapear.SetActive(false);
            exitbutton.SetActive(false);
            movement.enabled = false;
            watermeter.enabled = false;
        }
    }
}
