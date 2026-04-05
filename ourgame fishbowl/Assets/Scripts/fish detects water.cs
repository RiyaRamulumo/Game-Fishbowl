using UnityEngine;

public class FishDetectsWater : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            Debug.Log("Fish is in the water!");
        }
    }
}