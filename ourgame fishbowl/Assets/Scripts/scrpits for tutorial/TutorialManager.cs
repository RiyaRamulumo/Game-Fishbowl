using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;

    public float timeToShow = 30f;
    private float timer = 0f;
    private bool hasShown = false;

    void Update()
    {
        if (hasShown) return;

        timer += Time.deltaTime;

        if (timer >= timeToShow)
        {
            ShowTutorialMenu();
        }
    }

    // 🧻 SHOW MENU (used by timer + toilet)
    public void ShowTutorialMenu()
    {
        hasShown = true;

        if (tutorialPanel != null)
            tutorialPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    // BUTTONS

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