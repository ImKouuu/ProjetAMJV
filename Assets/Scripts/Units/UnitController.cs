using Unity.VisualScripting;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float speed;
    [SerializeField] private int mana;
    [SerializeField] private int maxMana;
    [SerializeField] private int maxHealth;
    [SerializeField] private int attackRange;
    [SerializeField] private int cost;
    [SerializeField] private int specialAttackDamage;

    void Update()
    {
        if (health <= 0)
        {
            Death();
        }
    }

    public int GetHealth() => health;
    public int GetAttackDamage() => attackDamage;
    public float GetAttackSpeed() => attackSpeed;
    public float GetSpeed() => speed;
    public int GetMana() => mana;
    public int GetMaxMana() => maxMana;
    public int GetMaxHealth() => maxHealth;
    public int GetAttackRange() => attackRange;
    public int GetCost() => cost;
    public int GetSpecialAttackDamage() => specialAttackDamage;


    // Setters
    public void SetHealth(int value) => health = value;
    public void SetAttackDamage(int value) => attackDamage = value;
    public void SetAttackSpeed(float value) => attackSpeed = value;
    public void SetSpeed(float value) => speed = value;
    public void SetMana(int value) => mana = value;
    public void SetMaxMana(int value) => maxMana = value;
    public void SetMaxHealth(int value) => maxHealth = value;
    public void SetAttackRange(int value) => attackRange = value;
    public void SetCost(int value) => cost = value;
    public void SetSpecialAttackDamage(int value) => specialAttackDamage = value;


    // Example functions
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0) health = 0;
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth) health = maxHealth;
    }

    public void UseMana(int amount)
    {
        mana -= amount;
        if (mana < 0) mana = 0;
    }

    public void RegenerateMana(int amount)
    {
        mana += amount;
        if (mana > maxMana) mana = maxMana;
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
