using UnityEngine;
using System.Collections;
using System;

public abstract class Attack : MonoBehaviour
{
    [SerializeField] public UnitStats unitStats;
    [SerializeField] public Animator animator;

    private string enemyTag;
     public UnitController unitController;

    public virtual void Start()
    {
        enemyTag = CompareTag("Unit") ? "Enemy" : "Unit";
        unitController = GetComponent<UnitController>();
    }
    public virtual void Launch(Transform target, Animator animator)
    {
        Debug.Log($"{gameObject.name} attaque {target.name}.");
        animator.Play("Attack");
        target.GetComponent<UnitController>().TakeDamage(unitStats.attackDamage);
        unitController.RegenerateMana();
    }
}
