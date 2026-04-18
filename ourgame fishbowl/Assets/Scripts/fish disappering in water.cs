using UnityEngine;

public class fishdisapperinginwater : MonoBehaviour
{
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>(); // get the fish image
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            sr.enabled = false; // hide fish
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            sr.enabled = true; // show fish again
        }
    }
}
