using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Watermeter : MonoBehaviour
{
    public Slider slider;
    public float sliderTime = 10f;
    private float currentTime;
    public bool inwater = false;
    public bool pause;
    private bool dead;

    public GameObject youlost;
    public GameObject disapear;
    public GameObject exitbutton;
    public GameObject pausemenu;
    public Movement movement;
    public TextMeshProUGUI howlost;
    public Watermeter watermeter;


    private void Start()
    {
        currentTime = sliderTime;
        slider.maxValue = sliderTime;
        slider.value = currentTime;
        dead = false;
    }

    public void Update()
    {
        if (pause) return;
       if (inwater == true)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime -= Time.deltaTime;
        }

        currentTime = Mathf.Clamp(currentTime, 0, sliderTime);

        slider.value = currentTime;

        if (currentTime <= 0 && dead == false)
        {
            youlost.SetActive(true);
            pausemenu.SetActive(true);
            howlost.text = "You suffocated on dry land";
            

            exitbutton.SetActive(false);
            movement.enabled = false;
            watermeter.enabled = false;
        }

        
    }
    public void PauseTime()
    {
        pause = true;
    }
    public void UnPause()
    {
        pause = false;
    }

    public void FullRefill()
    {
        currentTime = sliderTime;
        slider.value = currentTime; 
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            inwater = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            inwater = false;
        }
    }

    /*
    public void StartTimer()
    {
        StartCoroutine(StartTheTimer());

        IEnumerator StartTheTimer()
        {
            while (Stoptimer == false)
            {
                sliderTime -= Time.deltaTime;
                yield return new WaitForSeconds(0.001f);

                if (sliderTime >= 0)
                {
                    Stoptimer = true;
                }

                if (Stoptimer == false)
                {
                    slider.value = sliderTime;
                }
            }
        }
        
    }

    public void StopTimer()
    {
        Stoptimer = true;
    } */
}
