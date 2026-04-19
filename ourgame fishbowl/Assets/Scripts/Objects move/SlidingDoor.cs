using System.Collections;
using TMPro;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    [SerializeField] Animator animator;
    public SpriteRenderer JordgeJnr;
    public Sprite Jordge;
   

    public float speedopen = 2f;

    private bool doorOpen = false;
    private bool Playerin = false;

    public GameObject youlost;
    public GameObject disapear;
    public GameObject exitbutton;
    public GameObject pausemenu;
    public Movement movement;
    public TextMeshProUGUI howlost;
    private void Start()
    {
        animator.SetTrigger("Play");
    }

    public void DoorOpened()
    {
        doorOpen = true;
    }   

    public void DoorClosed()
    {
        doorOpen = false;
        StartCoroutine(Door());
    }

    IEnumerator Door()
    {
        
            yield return new WaitForSeconds(speedopen);
            animator.SetTrigger("Play");

            
    }

    private void Update()
    {
        if (Playerin && doorOpen == true)
        {
            float upDown = Input.GetAxis("Vertical");
            float sideways = Input.GetAxis("Horizontal");

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            {
                youlost.SetActive(true);
                pausemenu.SetActive(true);
                howlost.text = "You got caught by Jordge Jnr";
                JordgeJnr.sprite = Jordge;
                disapear.SetActive(false);
                exitbutton.SetActive(false);
                movement.enabled = false;
                Time.timeScale = 1f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
                Playerin = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Playerin = false;
        }
    }


}
