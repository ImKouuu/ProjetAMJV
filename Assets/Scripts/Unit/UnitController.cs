using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] private UnitStats unitStats;
    private int health;
    private int mana;

    private void Start()
    {
        health = unitStats.maxHealth;
        mana = 0;
    }

    private void Update()
    {
        if (health <= 0)
        {
            Death();
        }
    }

    public int GetHealth() => health;
    public int GetMana() => mana;

    public void SetHealth(int value) => health = Mathf.Clamp(value, 0, unitStats.maxHealth);
    public void SetMana(int value) => mana = Mathf.Clamp(value, 0, unitStats.maxMana);

    public void RegenerateMana() => SetMana(mana + unitStats.manaRegen);
    public void TakeDamage(int damage) => SetHealth(health + (unitStats.armor - damage));
    public void Heal(int amount) => SetHealth(health + amount);
    public void UseMana(int amount) => SetMana(mana - amount);

    private void Death() => Destroy(gameObject);
}