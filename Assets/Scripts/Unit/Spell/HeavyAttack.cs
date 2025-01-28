using UnityEngine;

public class HeavyAttack : Spell
{
    [SerializeField] private float specialAttackDamage;
    [SerializeField] private float radius = 5f;
    [SerializeField] private float force = 700f;

    public override void CastSpell(Transform target, Animator animator)
    {
        base.CastSpell(target, animator);
        PerformHeavyAttack();
    }

    private void PerformHeavyAttack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                UnitController enemyController = hitCollider.GetComponent<UnitController>();
                if (enemyController != null)
                {
                    enemyController.TakeDamage((int)specialAttackDamage);
                }

                Rigidbody rb = hitCollider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 direction = hitCollider.transform.position - transform.position;
                    rb.AddForce(direction.normalized * force);
                }
            }
        }
    }
}
