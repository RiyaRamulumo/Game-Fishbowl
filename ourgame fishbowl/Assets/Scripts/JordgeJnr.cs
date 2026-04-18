using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JordgeJnr : MonoBehaviour
{
    public GameObject youlost;
    public GameObject disapear;
    public GameObject exitbutton;
    public GameObject pausemenu;
    public Movement movement;
    public Sprite Jordgecaughtyou;
    public TextMeshProUGUI howlost;
    public SpriteRenderer Jordge;
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            youlost.SetActive(true);
            pausemenu.SetActive(true);
            howlost.text = "You were caught by Jordge Jnr";
            Jordge.sprite = Jordgecaughtyou;
            disapear.SetActive(false);
            exitbutton.SetActive(false);
            movement.enabled = false;
        } 
    }
}
