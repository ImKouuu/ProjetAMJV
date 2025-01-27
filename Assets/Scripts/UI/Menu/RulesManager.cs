using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class RulesManager : MonoBehaviour
{
    [SerializeField] private Button[] arenaButtons;
    [SerializeField] private Button launchButton, cheatButton;
    private int arenaIndex = 0;
    private Color defaultColor = Color.white;
    private Color selectedColor = new Color(0.43f, 0.71f, 1f, 1f);
    private Color cheatColor = new Color(1f, 0.5f, 0.5f, 1f);
    private bool cheatMode = false;

    private void OnEnable()
    {
        for (int i = 0; i < arenaButtons.Length; i++)
        {
            int index = i + 1; 
            arenaButtons[i].onClick.AddListener(() => ArenaButtonClicked(index));
        }
        launchButton.onClick.AddListener(LaunchButton);
        cheatButton.onClick.AddListener(CheatButton);
    }

    private void OnDisable()
    {
        foreach (var button in arenaButtons)
        {
            button.onClick.RemoveAllListeners();
        }
        launchButton.onClick.RemoveAllListeners();
        cheatButton.onClick.RemoveAllListeners();
        arenaIndex = 0;
    }

    private void ArenaButtonClicked(int index)
    {
        arenaIndex = index;
        UpdateButtonVisuals();
    }

    private void LaunchButton()
    {
        if (arenaIndex == 0)
        {
            Debug.LogError("No arena selected");
            return;
        }
        if (cheatMode)
        {
            MoneyManager.Instance.UnlimitedMoney();
        }
        StartCoroutine(LoadSceneCoroutine(arenaIndex));
    }

    private void CheatButton()
    {
        if (cheatMode)
        {
            cheatMode = false;
            cheatButton.image.color = defaultColor;
        }
        else
        {
            cheatMode = true;
            cheatButton.image.color = cheatColor;
        }
    }

    private void UpdateButtonVisuals()
    {
        for (int i = 0; i < arenaButtons.Length; i++)
        {
            var colors = arenaButtons[i].colors;
            
            if (i + 1 == arenaIndex)
            {
                colors.normalColor = selectedColor;
                colors.highlightedColor = selectedColor;
                colors.pressedColor = selectedColor;
                colors.selectedColor = selectedColor;
            }
            else
            {
                colors.normalColor = defaultColor;
                colors.highlightedColor = defaultColor;
                colors.pressedColor = defaultColor;
                colors.selectedColor = defaultColor;
            }
            
            arenaButtons[i].colors = colors;
        }
    }

    IEnumerator LoadSceneCoroutine(int index)
    {
        var op = SceneManager.LoadSceneAsync(index);
        while (!op.isDone)
        {
            yield return null;
        }
    }
}