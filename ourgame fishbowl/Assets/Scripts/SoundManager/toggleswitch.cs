using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class toggleswitch : MonoBehaviour
{
    public Image checkmark;
    private bool bswitch;
    public Sprite on;
    public Sprite off;

    public Toggle sfxtoggle;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bool sfxOn = PlayerPrefs.GetInt("SFX",1) == 1;
        sfxtoggle.isOn = sfxOn;

       Audiomanager.SFXOn = sfxOn;
        checkmark.sprite = sfxOn ? on : off;

        sfxtoggle.onValueChanged.AddListener(OnToggleChanged);
     
    }

    void OnToggleChanged(bool isOn)
    {
        Audiomanager.SFXOn = isOn;
        PlayerPrefs.SetInt("SFX", isOn ? 1 : 0);

        checkmark.sprite = isOn ? on : off;
    }
  
   

}
