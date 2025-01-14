using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class UnitMovement : MonoBehaviour
{
    public enum MovementMode { Offensive, Defensive, Neutral }

    [SerializeField] private MovementMode currentMode = MovementMode.Neutral;
    [SerializeField] private Transform target;

    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private UnitStats unitStats;
    private bool isAttacking = false;
    private UnitController unitController;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        unitController = transform.GetComponent<UnitController>();
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
        if (distance > unitStats.attackRange)
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
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, unitStats.attackRange);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Enemy"))
                {
                    if (unitController.GetMana() == unitStats.maxMana)
                    {
                        transform.GetComponent<Attack>().Launch(hitCollider.transform, GetComponent<Animator>(), true);
                        hitCollider.GetComponent<UnitController>().TakeDamage(unitStats.specialAttackDamage);
                        unitController.SetMana(0);
                    }
                    else
                    {
                        transform.GetComponent<Attack>().Launch(hitCollider.transform, GetComponent<Animator>(), false);
                        hitCollider.GetComponent<UnitController>().TakeDamage(unitStats.attackDamage);
                        unitController.RegenerateMana();
                    }
                }
            }
            yield return new WaitForSeconds(unitStats.attackSpeed);
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