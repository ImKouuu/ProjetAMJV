using UnityEngine;
using System;

public abstract class Spell : MonoBehaviour
{    
    [SerializeField] public UnitStats unitStats;
    [SerializeField] public Animator animator;

    public string enemyTag;
    [NonSerialized] public UnitController unitController;

    public virtual void Start()
    {
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
