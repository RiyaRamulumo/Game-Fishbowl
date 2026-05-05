using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Audiomanager : MonoBehaviour
{
    public static Audiomanager instance;
    public static bool SFXOn = true;

    public AudioSource audioSource;
    public AudioSource SFXSound;
    public Slider slider;
    public AudioClip death;
    public AudioClip jump;
    public AudioClip won;
    public AudioClip hitbubble;
    public AudioClip hitthefloor;
    public AudioClip background;
    public AudioClip splash;


    // Start is called once before the first execution of Update after the MonoBehaviour is created



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

        SFXOn = PlayerPrefs.GetInt("SFX", 1) == 1;
    }
    //public AudioClip soundeffects;
    void Start()
    {
        audioSource.clip = background;
        audioSource.Play();



        if (PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);


        }

        load();

        AudioListener.volume = slider.value;

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

    public void PlaySFX(AudioClip clip)
    {
        if (!SFXOn) return;
        SFXSound.PlayOneShot(clip);
    }



}
