using UnityEngine;

public class Datamanager : MonoBehaviour
{
    public static Datamanager instance;
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

    public void setVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
