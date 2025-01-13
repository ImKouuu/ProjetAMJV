using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitStats stats; // Référence au ScriptableObject

    private HealthComponent healthComponent;
    private MovementComponent movementComponent;
    private CombatComponent combatComponent;

    void Start()
    {
        // Initialisation des composants
        healthComponent = GetComponent<HealthComponent>();
        movementComponent = GetComponent<MovementComponent>();
        combatComponent = GetComponent<CombatComponent>();

        // Appliquer les stats du ScriptableObject
        if (healthComponent != null)
            healthComponent.SetMaxHealth(stats.GetCurrentHealth());

        if (movementComponent != null)
            movementComponent.SetSpeed(stats.GetSpeed());

        if (combatComponent != null)
            combatComponent.SetDamage(stats.GetDamage());
    }
}
