using Unity.VisualScripting;
using UnityEngine;

public class Heal : Spell
{
    [SerializeField] private float healRange = 1000f;

    public override void Start()
    {
        base.Start();
        unitController = GetComponent<UnitController>();

    }
    public override void CastSpell(Transform target, Animator animator)
    {
        UnitController nearestAlly = FindNearestAlly();
        if (nearestAlly != null)
        {
            animator.Play("Spell");
            unitController.SetMana(0);
            nearestAlly.Heal(unitStats.specialAttackDamage);
        }
        else
        {
            animator.Play("Spell");
            unitController.SetMana(0);
            transform.GetComponent<UnitController>().Heal(unitStats.specialAttackDamage/2);
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
