using UnityEngine;

public class Heal : Spell
{
    [SerializeField] private int healAmount;
    [SerializeField] private float healRange;

    public override void CastSpell(Transform target, Animator animator)
    {
        UnitController nearestAlly = FindNearestAlly();
        if (nearestAlly != null)
        {
            base.CastSpell(target, animator);
            nearestAlly.Heal(healAmount);
            Debug.Log($"{gameObject.name} a soigné {nearestAlly.name} pour {healAmount} points de vie.");
        }
        else
        {
            Debug.Log("Aucun allié à portée pour être soigné.");
        }
    }

    private UnitController FindNearestAlly()
    {
        UnitController nearestAlly = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject potentialAlly in GameObject.FindGameObjectsWithTag(gameObject.tag))
        {
            if (potentialAlly == gameObject) continue;

            float distance = Vector3.Distance(transform.position, potentialAlly.transform.position);
            if (distance < healRange && distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestAlly = potentialAlly.GetComponent<UnitController>();
            }
        }

        return nearestAlly;
    }
}
