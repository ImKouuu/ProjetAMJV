using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    [SerializeField] private int money = 0;
    private bool unlimitedMoney = false;
    private int savedMoney = 0; // Variable pour stocker l'argent

    private int moneyBeforeShop = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetMoney()
    {
        return money;
    }

    public void AddMoney(int amount)
    {
        money += amount;
        SaveMoney(); // Sauvegarder l'argent après chaque modification
    }

    public void SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            SaveMoney(); // Sauvegarder l'argent après chaque modification
        }
        else
        {
            Debug.LogWarning("Not enough money!");
        }
    }

    public bool IsMoneyUnlimited()
    {
        return unlimitedMoney;
    }

    public void UnlimitedMoney()
    {
        money = int.MaxValue;
        unlimitedMoney = true;
    }

    public void ResetMoney()
    {
        money = 0;
        unlimitedMoney = false;
    }

    public void SaveMoney()
    {
        savedMoney = money;
    }

    public void TurnSavedMoneyIntoMainMoney()
    {
        money = savedMoney;
    }

    public void SaveMoneyBeforeShop()
    {
        moneyBeforeShop = money;
    }

    public void TurnMoneyBeforeShopIntoMainMoney()
    {
        money = moneyBeforeShop;
    }
}