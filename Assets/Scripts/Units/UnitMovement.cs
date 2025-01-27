using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEditor.Callbacks;

public class UnitMovement : MonoBehaviour
{
    public enum MovementMode { Offensive, Defensive, Neutral }

    [SerializeField] private MovementMode currentMode = MovementMode.Neutral;
    [SerializeField] private Transform target;

    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private UnitStats unitStats;
    private bool isAttacking = false;
    private UnitController unitController;
    [SerializeField] private Animator animator;
    private float currentSpeed;
    private float smoothing = 10f;
    private new string tag;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        unitController = transform.GetComponent<UnitController>();
        animator = GetComponent<Animator>();
        if (transform.CompareTag("Unit"))
        {
            tag = "Enemy";
        }
        else
        {
            tag = "Unit";
        }
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
        float targetSpeed = GetComponent<Rigidbody>() != null ? GetComponent<Rigidbody>().linearVelocity.magnitude : 0f;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * smoothing);
        animator.SetFloat("Speed", currentSpeed);
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
            navMeshAgent.SetDestination(transform.position); 
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
                if (hitCollider.CompareTag(tag))
                {
                    if (unitController.GetMana() == unitStats.maxMana)
                    {
                        transform.GetComponent<Attack>().Launch(hitCollider.transform, animator, true);
                        hitCollider.GetComponent<UnitController>().TakeDamage(unitStats.specialAttackDamage);
                        unitController.SetMana(0);
                    }
                    else
                    {
                        transform.GetComponent<Attack>().Launch(hitCollider.transform, animator, false);
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
        foreach (GameObject potentialTarget in GameObject.FindGameObjectsWithTag(tag))
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