using UnityEngine;

public class waterbubblepop2 : MonoBehaviour
{
    [SerializeField] private Animator waterbubble;
    [SerializeField] private Watermeter watermeter;
    public GameObject orb2;

    private bool popped = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DisableObjects()
    {
      orb2.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (popped) return;

        if (other.CompareTag("Player"))
        {
            Audiomanager.instance.PlaySFX(Audiomanager.instance.hitbubble);
            popped = true;
            waterbubble.SetTrigger("pop2");

            watermeter.FullRefill();

        }
    }
}
