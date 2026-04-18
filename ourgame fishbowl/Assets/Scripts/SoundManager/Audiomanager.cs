using UnityEngine;
using UnityEngine.UI;

public class Audiomanager : MonoBehaviour
{
    public static Audiomanager instance;
    
    [SerializeField] AudioSource audioSource;
    [SerializeField] Slider slider;
    //[SerializeField] AudioSource SFX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public AudioClip background;

    private void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);

        }
    }
    //public AudioClip soundeffects;
    void Start()
    {
        audioSource.clip = background;
        audioSource.Play();



        if (PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            load();

        }
        else
        {
            load();

            AudioListener.volume = slider.value;
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = slider.value;
        save();
    }

    private void load()
    {
       slider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void save()
    {
        PlayerPrefs.SetFloat("musicVolume", slider.value);
    }

   

}
