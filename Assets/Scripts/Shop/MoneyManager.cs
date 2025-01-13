using UnityEngine;
using System.Collections;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    [SerializeField] private int money = 0;

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

    void Start()
    {
        StartCoroutine(AddMoneyOverTime());
    }

    private IEnumerator AddMoneyOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            money++;
            Debug.Log("Money: " + money);
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
}