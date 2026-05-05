using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class mousetrap : MonoBehaviour
{
    public GameObject youlost;
    public GameObject disapear;
    public GameObject exitbutton;
    public GameObject pausemenu;
    public Light2D light2D;

    public Movement movement;
    public TextMeshProUGUI howlost;
    public Watermeter watermeter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            light2D.intensity = 1;
            youlost.SetActive(true);
            pausemenu.SetActive(true);
            howlost.text = "You got caught in a mouse trap";
            Audiomanager.instance.PlaySFX(Audiomanager.instance.death);
            disapear.SetActive(false);
            exitbutton.SetActive(false);
            movement.enabled = false;
            watermeter.enabled = false;
        }
    }
}
