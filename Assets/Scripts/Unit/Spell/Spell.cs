using UnityEngine;
using System;

public abstract class Spell : MonoBehaviour
{    
    [SerializeField] public UnitStats unitStats;
    [SerializeField] public Animator animator;

    [NonSerialized] public string enemyTag;
    public UnitController unitController;

    public virtual void Start()
    {
        unitStats = GetComponent<UnitStats>();
        animator = GetComponent<Animator>();
        enemyTag = CompareTag("Unit") ? "Enemy" : "Unit";
        unitController = GetComponent<UnitController>();
    }
    public virtual void CastSpell(Transform target, Animator animator)
    {
        animator.Play("Spell");
        target.GetComponent<UnitController>().TakeDamage(unitStats.specialAttackDamage);
        unitController.SetMana(0);
    }
}
