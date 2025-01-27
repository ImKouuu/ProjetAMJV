using UnityEngine;
using UnityEngine.UI;

public class ClosePanel : MonoBehaviour
{
    private Button returnButton;

    private void Awake()
    {
        returnButton = GetComponent<Button>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        returnButton.onClick.AddListener(ReturnButton);
    }

    private void OnDisable()
    {
        returnButton.onClick.RemoveListener(ReturnButton);
    }

    public void ReturnButton()
    {
        transform.parent.gameObject.SetActive(false);
    }
}