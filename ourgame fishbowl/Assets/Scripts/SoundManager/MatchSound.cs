using UnityEngine;
using UnityEngine.UI;

public class MatchSound : MonoBehaviour
{
    public Slider forsound;
    private float volumeforsound;

    [SerializeField] AudioSource source;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        volumeforsound = 1f;
    }
    
    

    // Update is called once per frame
    void Update()
    {
        if (forsound != null)
        {
            volumeforsound = forsound.value;
        }
        source.volume = volumeforsound;
    }
}
