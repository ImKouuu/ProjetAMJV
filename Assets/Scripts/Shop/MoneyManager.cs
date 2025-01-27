using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    [SerializeField] private int money = 0;
    private bool unlimitedMoney = false;

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
    }

    public void SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
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
}