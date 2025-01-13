using UnityEngine;

[CreateAssetMenu(fileName = "UnitStats", menuName = "Units/Stats")]
public class UnitStats : ScriptableObject
{
    [SerializeField] private string unitName;
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private float speed;

    public float GetCurrentHealth()
    {
        return health;
    }

    public float GetDamage()
    {
        return damage;
    }

    public float GetSpeed()
    {
        return speed;
    }
}
