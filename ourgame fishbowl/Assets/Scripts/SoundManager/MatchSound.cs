using UnityEngine;
using UnityEngine.UI;

public class MatchSound : MonoBehaviour
{
    public Slider forsound;
    public Slider tomatch;
    [SerializeField] AudioSource source; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      tomatch.value = forsound.value;  
      source.volume = tomatch.value;
     
    }
}
