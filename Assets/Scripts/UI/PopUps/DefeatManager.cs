using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DefeatManager : MonoBehaviour
{
    [SerializeField] private GameObject defeatPanel;
    [SerializeField] private Button backToMenuButton, restartButton;
    [SerializeField] private TMP_Text damageStatsText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        backToMenuButton.onClick.AddListener(BackToMenuButton);
        restartButton.onClick.AddListener(RestartButton);
    }

    // Update is called once per frame
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
