using UnityEngine;

public class CombatComponent : MonoBehaviour
{
    [SerializeField] private float damage;

    public void Attack(GameObject target)
    {
        var targetHealth = target.GetComponent<HealthComponent>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
        }
    }
}
