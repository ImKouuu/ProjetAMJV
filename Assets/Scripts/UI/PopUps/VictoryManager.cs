using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class VictoryManager : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private Button backToMenuButton, nextArenaButton;
    [SerializeField] private TMP_Text victoryText, damageStatsText;
    [SerializeField] private List<int> moneyRewards = new List<int> { 100, 200, 300, 400 };
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        backToMenuButton.onClick.AddListener(BackToMenuButton);
        nextArenaButton.onClick.AddListener(NextArenaButton);
    }

    // Update is called once per frame
    private void OnDisable()
    {
        backToMenuButton.onClick.RemoveListener(BackToMenuButton);
        nextArenaButton.onClick.RemoveListener(NextArenaButton);
    }

    private void BackToMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void NextArenaButton()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Vérifiez si l'index de la scène suivante est valide
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Time.timeScale = 1;
            DamageStatsManager.Instance.ResetDamageStats();
            DamageStatsManager.Instance.SetDamageStatsText(damageStatsText);
            MoneyManager.Instance.AddMoney(moneyRewards[currentSceneIndex]);
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            victoryText.text = "Vous avez terminé le jeu !";
            nextArenaButton.gameObject.SetActive(false);
        }
    }
}
