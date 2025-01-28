using UnityEngine;

public class HeavyAttack : Spell
{
    [SerializeField] private float radius = 5f;
    [SerializeField] private float force = 400f;

    public override void Start()
    {
        base.Start();
    }

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
            if (hitCollider.tag == enemyTag)
            {
                UnitController enemyController = hitCollider.GetComponent<UnitController>();
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
