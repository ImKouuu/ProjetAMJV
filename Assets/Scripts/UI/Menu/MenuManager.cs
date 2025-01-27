using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button playButton, optionButton, quitButton;
    [SerializeField] private GameObject optionPanel, rulesPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        playButton.onClick.AddListener(PlayButton);
        optionButton.onClick.AddListener(OptionButton);
        quitButton.onClick.AddListener(QuitButton);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(PlayButton);
        optionButton.onClick.RemoveListener(OptionButton);
        quitButton.onClick.RemoveListener(QuitButton);
    }

    public void PlayButton()
    {
        rulesPanel.SetActive(true);
    }

    public void QuitButton()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void OptionButton()
    {
        optionPanel.SetActive(true);
    }
}
