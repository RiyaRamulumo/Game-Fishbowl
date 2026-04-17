using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor.Build.Content;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using UnityEditorInternal;
using UnityEditor;

public class leveloader : MonoBehaviour
{
    public GameObject loadingscreen;
    public Slider slider;
    public TextMeshProUGUI progresstext;
    public Animator animator;
    
    public float transitiontime = 2f;
    public bool ToPlay = false;

   
    public void Triggeranimation()
    {
        animator.SetTrigger("play");
    }
  
    public void LoadLevel (int sceneIndex) 
    {
        
       StartCoroutine(LoadAsynchronously(sceneIndex));
       
    }

    IEnumerator LoadAsynchronously (int sceneIndex)
    {
      

        yield return new WaitForSeconds(transitiontime);
      
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingscreen.SetActive(true);

        while (operation.isDone == false)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            progresstext.text = progress * 100f + "%";
            
            if (progress == 100)
            {
                animator.SetTrigger("Done playing");
            }

            yield return null;
        }
    }
}
