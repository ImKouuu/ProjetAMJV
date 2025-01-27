using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button continueButton, backToMenuButton;
    private bool isPaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private bool isEscapePressed()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }

    // Update is called once per frame
    void Update()
    {
        if (isEscapePressed())
        {
            isPaused = !isPaused;
        }
        if (isPaused)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
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
    }

    private void BackToMenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
