using UnityEngine;

[CreateAssetMenu(fileName = "UnitStats", menuName = "Scriptable Objects/UnitStats")]
public class UnitStats : ScriptableObject
{
    public int attackDamage;
    public float attackSpeed;
    public float speed;
    public int maxMana;
    public int maxHealth;
    public int attackRange;
    public int cost;
    public int specialAttackDamage;
    public int armor;
    public int manaRegen;
}
