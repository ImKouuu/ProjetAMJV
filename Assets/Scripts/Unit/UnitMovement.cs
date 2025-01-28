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
    [SerializeField] private Animator animator;

    private UnitController unitController;
    private float currentSpeed;
    private float smoothing = 10f;
    private string enemyTag;
    private bool isAttacking = false;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        unitController = GetComponent<UnitController>();
        animator = GetComponent<Animator>();

        enemyTag = CompareTag("Unit") ? "Enemy" : "Unit";
    }

    private void Update()
    {
        HandleMovement();
        UpdateAnimatorSpeed();
    }

    public MovementMode GetCurrentMode()
    {
        return currentMode;
    }

    public void SetMovementMode(MovementMode mode)
    {
        currentMode = mode;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void HandleMovement()
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

    private void MoveOffensive()
    {
        if (target != null)
        {
            navMeshAgent.SetDestination(target.position);
            AttackEnemiesInRange();
        }
    }

    private void MoveDefensive()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance > unitStats.attackRange)
            {
                navMeshAgent.SetDestination(target.position);
            }
            else
            {
                navMeshAgent.SetDestination(transform.position);
                AttackEnemiesInRange();
            }
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

    private void UpdateAnimatorSpeed()
    {
        float targetSpeed = navMeshAgent.velocity.magnitude;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * smoothing);
        animator.SetFloat("Speed", currentSpeed);
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
                if (hitCollider.CompareTag(enemyTag))
                {
                    PerformAttack(hitCollider);
                }
            }
            yield return new WaitForSeconds(unitStats.attackSpeed);
        }
    }

    private void PerformAttack(Collider targetCollider)
    {
        if (unitController.GetMana() == unitStats.maxMana)
        {
            GetComponent<Spell>().CastSpell(targetCollider.transform, animator);
            targetCollider.GetComponent<UnitController>().TakeDamage(unitStats.specialAttackDamage);
        }
        else
        {
            GetComponent<Attack>().Launch(targetCollider.transform, animator);
            targetCollider.GetComponent<UnitController>().TakeDamage(unitStats.attackDamage);
        }
    }

    private Transform FindClosestTarget()
    {
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject potentialTarget in GameObject.FindGameObjectsWithTag(enemyTag))
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
