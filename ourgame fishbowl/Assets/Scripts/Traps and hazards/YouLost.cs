using TMPro;
using UnityEngine;

public class YouLost : MonoBehaviour
{
    public GameObject youlost;
    public GameObject disapear;
    public GameObject exitbutton;
    public GameObject pausemenu;
    public Movement movement;
    public TextMeshProUGUI howlost;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            youlost.SetActive(true) ;
            pausemenu.SetActive(true) ;
            howlost.text = "You woke up Jordge Snr";
            
            disapear.SetActive(false) ;
            exitbutton.SetActive(false) ;
            movement.enabled = false ;
        }
    }
}
