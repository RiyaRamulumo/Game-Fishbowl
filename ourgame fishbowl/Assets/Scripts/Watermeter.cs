using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Watermeter : MonoBehaviour
{
    public Slider slider;
    public float sliderTime = 10f;
    private float currentTime;
    public bool inwater = false;
    public bool pause;
    private bool dead;
    private int seconds;
    public TextMeshProUGUI timer;
    public GameObject youlost;
    public GameObject disapear;
    public GameObject exitbutton;
    public GameObject pausemenu;
    public Movement movement;
    public TextMeshProUGUI howlost;
    public Watermeter watermeter;
   public Light2D light2D;
    public Image waterguy;
    public Sprite happy;
    public Animator waterripple;
    public GameObject twaterripple;
   
    
    public Sprite Worried;
    public Sprite Scared;

    float Intensity;
    public float changelight = 2f;
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
        seconds = Mathf.CeilToInt(currentTime);
        timer.text = "00:" + seconds.ToString("00");

        if (seconds > 4 && seconds < 7)
        {
            waterguy.sprite = happy;
            timer.color = Color.white;
            Intensity = 1f;
        }
        if (seconds > 2 && seconds <= 4)
        {
            waterguy.sprite = Worried;
            timer.color = Color.red;
            Intensity = 0.6f;
        }
        if (seconds > 0 && seconds <= 2)
        {
            waterguy.sprite = Scared;
            timer.color = Color.darkRed;
            Intensity = 0.2f;
        }

        light2D.intensity = Mathf.Lerp(light2D.intensity, Intensity, Time.deltaTime * changelight);
        
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
            Audiomanager.instance.PlaySFX(Audiomanager.instance.splash);
          
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Bucket 1"))
        {
            twaterripple.transform.position = new Vector3(8.256f, -2.9495f, 0);
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
