using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class UnitMovement : MonoBehaviour
{
    public enum MovementMode { Offensive, Defensive, Neutral }

    [SerializeField] private MovementMode currentMode = MovementMode.Neutral;
    [SerializeField] private Transform target;

    [SerializeField] private NavMeshAgent navMeshAgent;
    private bool isAttacking = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        switch (currentMode)
        {
            case MovementMode.Offensive:
                MoveOffensive();
                break;
            case MovementMode.Defensive:
                MoveDefensive();
                break;
            case MovementMode.Neutral:
                MoveNeutral();
                break;
        }
    }

    public void SetMovementMode(MovementMode mode)
    {
        currentMode = mode;
    }

    private void MoveOffensive()
    {
        navMeshAgent.SetDestination(target.position);
        AttackEnemiesInRange();
    }

    private void MoveDefensive()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance > transform.GetComponent<UnitController>().GetAttackRange())
        {
            navMeshAgent.SetDestination(target.position);
        }
        else
        {
            navMeshAgent.SetDestination(transform.position); // Stay in place
            AttackEnemiesInRange();
        }
    }

    private void MoveNeutral()
    {
        Transform closestTarget = FindClosestTarget();
        if (closestTarget != null)
        {
            navMeshAgent.SetDestination(closestTarget.position);
            AttackEnemiesInRange();
        }
    }

    private void AttackEnemiesInRange()
    {
        if (!isAttacking)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        while (true)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, transform.GetComponent<UnitController>().GetAttackRange());
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Enemy"))
                {
                    if (transform.GetComponent<UnitController>().GetMana() == transform.GetComponent<UnitController>().GetMaxMana())
                    {
                        hitCollider.GetComponent<UnitController>().TakeDamage(transform.GetComponent<UnitController>().GetSpecialAttackDamage());
                    }
                    else
                    {
                        hitCollider.GetComponent<UnitController>().TakeDamage(transform.GetComponent<UnitController>().GetAttackDamage());
                    }
                }
            }
            yield return new WaitForSeconds(transform.GetComponent<UnitController>().GetAttackSpeed());
        }
    }

    private Transform FindClosestTarget()
    {
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject potentialTarget in GameObject.FindGameObjectsWithTag("Unit"))
        {
            float distance = Vector3.Distance(transform.position, potentialTarget.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = potentialTarget.transform;
            }
        }
        return closestTarget;
    }
}