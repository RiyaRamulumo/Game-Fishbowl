using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;             
using UnityEngine.UI;
using TMPro;


public class leveloader : MonoBehaviour
{
    public GameObject loadingscreen;
    public Slider slider;
    public TextMeshProUGUI progresstext;
    public Animator animator;
    
    public float transitiontime = 2f;
    

   
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
            


            yield return null;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
      
    }
}
