using System.Collections;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class YouLost : MonoBehaviour
{
    public GameObject youlost;
    public GameObject disapear;
    public GameObject exitbutton;
    public GameObject pausemenu;
    public Movement movement;
    public Light2D light2D;
    public TextMeshProUGUI howlost;
    public Watermeter watermeter;
    public GameObject JordgeSnr;
    public GameObject JordgeSnrCaught;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {


            JordgeSnr.SetActive(false);
            JordgeSnrCaught.SetActive(true);
            movement.enabled = false;
            watermeter.enabled = false;


            Audiomanager.instance.PlaySFX(Audiomanager.instance.death);
            StartCoroutine(WaitAndLose());
         
        }
    }

    IEnumerator WaitAndLose()
    {
        yield return new WaitForSeconds(1); 
        light2D.intensity = 1;
            youlost.SetActive(true);  
            howlost.text = "You woke up Jordge Snr";
            pausemenu.SetActive(true);  
            disapear.SetActive(false);
            exitbutton.SetActive(false);
           
    }

 
}
