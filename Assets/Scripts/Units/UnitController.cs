using Unity.VisualScripting;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] private UnitStats unitStats;
    private int health;
    private int mana;

    void Start()
    {
        health = unitStats.maxHealth;
        mana = unitStats.maxMana;
    }
    void Update()
    {
        if (health <= 0)
        {
            Death();
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMana()
    {
        return mana;
    }

    public void SetHealth(int value)
    {
        health = value;
    }

    public void SetMana(int value)
    {
        mana = value;
    }

    // Example functions
    public void RegenerateMana()
    {
        mana +=unitStats.manaRegen;
        if (mana > unitStats.maxMana) mana = unitStats.maxMana;
    }
    public void TakeDamage(int damage)
    {
        health -= (unitStats.armor-damage);
        if (health < 0) health = 0;
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > unitStats.maxHealth) health = unitStats.maxHealth;
    }

    public void UseMana(int amount)
    {
        mana -= amount;
        if (mana < 0) mana = 0;
    }

    public void RegenerateMana(int amount)
    {
        mana += amount;
        if (mana > unitStats.maxMana) mana = unitStats.maxMana;
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
