using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button continueButton, backToMenuButton;
    private bool isPaused = false;

    private bool isEscapePressed()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }

    void Update()
    {
        if (isEscapePressed())
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
        }
        pausePanel.SetActive(true);
    }

    private void ResumeGame()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        pausePanel.SetActive(false);
    }

    private void OnEnable()
    {
        continueButton.onClick.AddListener(ContinueButton);
        backToMenuButton.onClick.AddListener(BackToMenuButton);
    }

    private void OnDisable()
    {
        continueButton.onClick.RemoveListener(ContinueButton);
        backToMenuButton.onClick.RemoveListener(BackToMenuButton);
    }

    private void ContinueButton()
    {
        isPaused = false;
        ResumeGame();
    }

    private void BackToMenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}