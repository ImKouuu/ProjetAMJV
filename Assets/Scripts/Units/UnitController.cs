using UnityEngine;
using UnityEngine.UI;

public class UnitController : MonoBehaviour
{
    [SerializeField] private UnitStats unitStats;
    [SerializeField] private GameObject healthManaCanvasPrefab;
    [SerializeField] private float barHeightOffset = 2f;
    private int health, mana, cost;
    private bool barsVisible = false;
    private Canvas healthManaCanvas;
    private Image healthBar, manaBar;

    private void Start()
    {
        health = unitStats.maxHealth;
        mana = 0;
        cost = unitStats.cost;

        CreateBars();

        UpdateHealthBar();
        UpdateManaBar();
        SetBarsVisible(false);
    }

    private void Update()
    {
        if (health <= 0 || transform.position.y < -10)
        {
            Death();
        }
    }

    public int GetHealth() => health;
    public int GetMana() => mana;
    public int GetCost() => cost;
    public void SetHealth(int value)
    {
        health = Mathf.Clamp(value, 0, unitStats.maxHealth);
        UpdateHealthBar();
        ShowBars();
    }
    public void SetMana(int value)
    {
        mana = Mathf.Clamp(value, 0, unitStats.maxMana);
        UpdateManaBar();
        ShowBars();
    }
    public void RegenerateMana() => SetMana(mana + unitStats.manaRegen);
    public void TakeDamage(int damage) => SetHealth(health + (unitStats.armor - damage));
    public void Heal(int amount) => SetHealth(health + amount);
    public void UseMana(int amount) => SetMana(mana - amount);

    private void Death()
    {
        if(gameObject.GetComponent<Crown>().enabled == true) 
        {
            if (gameObject.CompareTag("Unit"))
            {
                GameManager.Instance.SetBattleOver(isVictory: false);
            }
            else
            {
                GameManager.Instance.SetBattleOver(isVictory: true);
            }
        }
        Destroy(gameObject);
    }

    private void CreateBars()
    {
        GameObject canvasObject = Instantiate(healthManaCanvasPrefab, transform);
        healthManaCanvas = canvasObject.GetComponent<Canvas>();

        healthManaCanvas.transform.localPosition = new Vector3(0, barHeightOffset, 0);

        healthBar = healthManaCanvas.transform.Find("HealthBar").GetComponent<Image>();
        manaBar = healthManaCanvas.transform.Find("ManaBar").GetComponent<Image>();
    }

    private void UpdateHealthBar()
    {
        float healthRatio = (float)health / unitStats.maxHealth;
        healthBar.fillAmount = healthRatio; 
        healthBar.color = Color.Lerp(Color.red, Color.white, 1 - healthRatio); 
    }

    private void UpdateManaBar()
    {
        float manaRatio = (float)mana / unitStats.maxMana;
        manaBar.fillAmount = manaRatio; 
        manaBar.color = Color.Lerp(Color.white, Color.blue, manaRatio);
    }

    private void ShowBars()
    {
        if (!barsVisible)
        {
            SetBarsVisible(true);
        }
    }

    private void SetBarsVisible(bool visible)
    {
        healthManaCanvas.gameObject.SetActive(visible);
        barsVisible = visible;
    }

}