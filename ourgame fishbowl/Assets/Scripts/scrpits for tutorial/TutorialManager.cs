using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;   // The panel with buttons

    public float timeToShow = 30f;     // Time before showing panel
    private float timer = 0f;
    private bool hasShown = false;

    void Update()
    {
        if (hasShown) return;

        timer += Time.deltaTime;

        if (timer >= timeToShow)
        {
            ShowOptions();
        }
    }

    void ShowOptions()
    {
        hasShown = true;

        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(true);
        }

        // Pause the game
        Time.timeScale = 0f;
    }

    // BUTTON FUNCTIONS

    public void ContinueTutorial()
    {
        tutorialPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoToLevel1()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 1");
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}