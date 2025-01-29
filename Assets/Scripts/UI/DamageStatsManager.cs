using UnityEngine;
using TMPro;

public class DamageStatsManager : MonoBehaviour
{
    public static DamageStatsManager Instance { get; private set; }

    [SerializeField] private TMP_Text damageStatsText;
    private int humanDamage = 0;
    private int elfDamage = 0;
    private int dwarfDamage = 0;
    private int trollDamage = 0;
    private int dragonDamage = 0;
    private int tikiDamage = 0;
    private int grosDamage = 0;
    private int bombeDamage = 0;
    private int lanceDamage = 0;
    private int morsureDamage = 0;
    private int maoriDamage = 0;
    private int shamanDamage = 0;
    private int sarbacaneDamage = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        if (damageStatsText != null)
        {
            damageStatsText.gameObject.SetActive(false);
        }
    }

    public void SetDamageStatsText(TMP_Text newDamageStatsText)
    {
        damageStatsText = newDamageStatsText;
        if (damageStatsText != null)
        {
            damageStatsText.gameObject.SetActive(false);
        }
    }

    public void UpdateHumanDamage(int damage)
    {
        humanDamage += damage;
        UpdateDamageStatsText();
    }

    public void UpdateElfDamage(int damage)
    {
        elfDamage += damage;
        UpdateDamageStatsText();
    }

    public void UpdateDwarfDamage(int damage)
    {
        dwarfDamage += damage;
        UpdateDamageStatsText();
    }

    public void UpdateTrollDamage(int damage)
    {
        trollDamage += damage;
        UpdateDamageStatsText();
    }

    public void UpdateDragonDamage(int damage)
    {
        dragonDamage += damage;
        UpdateDamageStatsText();
    }

    public void UpdateTikiDamage(int damage)
    {
        tikiDamage += damage;
        UpdateDamageStatsText();
    }

    public void UpdateGrosDamage(int damage)
    {
        grosDamage += damage;
        UpdateDamageStatsText();
    }

    public void UpdateBombeDamage(int damage)
    {
        bombeDamage += damage;
        UpdateDamageStatsText();
    }

    public void UpdateLanceDamage(int damage)
    {
        lanceDamage += damage;
        UpdateDamageStatsText();
    }

    public void UpdateMorsureDamage(int damage)
    {
        morsureDamage += damage;
        UpdateDamageStatsText();
    }

    public void UpdateMaoriDamage(int damage)
    {
        maoriDamage += damage;
        UpdateDamageStatsText();
    }

    public void UpdateShamanDamage(int damage)
    {
        shamanDamage += damage;
        UpdateDamageStatsText();
    }

    public void UpdateSarbacaneDamage(int damage)
    {
        sarbacaneDamage += damage;
        UpdateDamageStatsText();
    }

    private void UpdateDamageStatsText()
    {
        if (damageStatsText != null)
        {
            damageStatsText.text = $"Humains : {humanDamage}\n" +
                                    $"Elfes : {elfDamage}\n" +
                                    $"Nains : {dwarfDamage}\n" +
                                    $"Trolls : {trollDamage}\n" +
                                    $"Dragons : {dragonDamage}\n" +
                                    $"Tikis : {tikiDamage}\n" +
                                    $"Gros : {grosDamage}\n" +
                                    $"Bombes : {bombeDamage}\n" +
                                    $"Lances : {lanceDamage}\n" +
                                    $"Morsures : {morsureDamage}\n" +
                                    $"Maoris : {maoriDamage}\n" +
                                    $"Shamans : {shamanDamage}\n" +
                                    $"Sarbacanes : {sarbacaneDamage}";
        }
    }

    public void ShowDamageStats()
    {
        if (damageStatsText != null)
        {
            damageStatsText.gameObject.SetActive(true);
        }
    }

    public void HideDamageStats()
    {
        if (damageStatsText != null)
        {
            damageStatsText.gameObject.SetActive(false);
        }
    }

    public void ResetDamageStats()
    {
        humanDamage = 0;
        elfDamage = 0;
        dwarfDamage = 0;
        trollDamage = 0;
        dragonDamage = 0;
        tikiDamage = 0;
        grosDamage = 0;
        bombeDamage = 0;
        lanceDamage = 0;
        morsureDamage = 0;
        maoriDamage = 0;
        shamanDamage = 0;
        sarbacaneDamage = 0;
        UpdateDamageStatsText();
    }
}