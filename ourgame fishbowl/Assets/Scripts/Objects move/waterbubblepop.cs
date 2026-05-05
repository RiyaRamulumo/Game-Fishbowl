using UnityEngine;

public class waterbubblepop : MonoBehaviour
{    
    [SerializeField] private Animator waterbubble;
    [SerializeField] private Watermeter watermeter;

    private bool popped = false;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {  
        if (popped) return;

        if (other.CompareTag("Player"))
        {

            popped = true;
            waterbubble.SetTrigger("pop");

            watermeter.FullRefill();

        }
    }
}
