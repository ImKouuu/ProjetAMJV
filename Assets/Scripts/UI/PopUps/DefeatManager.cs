using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DefeatManager : MonoBehaviour
{
    [SerializeField] private GameObject defeatPanel;
    [SerializeField] private Button backToMenuButton, restartButton;
    [SerializeField] private TMP_Text damageStatsText;

    private void OnEnable()
    {
        backToMenuButton.onClick.AddListener(BackToMenuButton);
        restartButton.onClick.AddListener(RestartButton);
    }

    private void OnDisable()
    {
        backToMenuButton.onClick.RemoveListener(BackToMenuButton);
        restartButton.onClick.RemoveListener(RestartButton);
    }

    private void BackToMenuButton()
    {
        Time.timeScale = 1;
        DamageStatsManager.Instance.ResetDamageStats();
        SceneManager.LoadScene(0);
        DamageStatsManager.Instance.SetDamageStatsText(damageStatsText);
    }

    private void RestartButton()
    {
        Time.timeScale = 1;
        MoneyManager.Instance.TurnMoneyBeforeShopIntoMainMoney();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}