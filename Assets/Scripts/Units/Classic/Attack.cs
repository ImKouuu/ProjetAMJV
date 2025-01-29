using UnityEngine;
using System.Collections;
using System;

public abstract class Attack : MonoBehaviour
{
    [SerializeField] public UnitStats unitStats;
    [SerializeField] public Animator animator;

    public string enemyTag;
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
        
        if (unitController.gameObject.CompareTag("Unit"))
        {
            string unitName = unitController.gameObject.name.ToLower();
            if (unitName.Contains("humain"))
            {
                DamageStatsManager.Instance.UpdateHumanDamage(unitStats.attackDamage);
            }
            else if (unitName.Contains("elfe"))
            {
                DamageStatsManager.Instance.UpdateElfDamage(unitStats.attackDamage);
            }
            else if (unitName.Contains("nain"))
            {
                DamageStatsManager.Instance.UpdateDwarfDamage(unitStats.attackDamage);
            }
            else if (unitName.Contains("troll"))
            {
                DamageStatsManager.Instance.UpdateTrollDamage(unitStats.attackDamage);
            }
            else if (unitName.Contains("dragon"))
            {
                DamageStatsManager.Instance.UpdateDragonDamage(unitStats.attackDamage);
            }
            else if (unitName.Contains("tiki"))
            {
                DamageStatsManager.Instance.UpdateTikiDamage(unitStats.attackDamage);
            }
            else if (unitName.Contains("gros"))
            {
                DamageStatsManager.Instance.UpdateGrosDamage(unitStats.attackDamage);
            }
            else if (unitName.Contains("bombe"))
            {
                DamageStatsManager.Instance.UpdateBombeDamage(unitStats.attackDamage);
            }
            else if (unitName.Contains("lance"))
            {
                DamageStatsManager.Instance.UpdateLanceDamage(unitStats.attackDamage);
            }
            else if (unitName.Contains("morsure"))
            {
                DamageStatsManager.Instance.UpdateMorsureDamage(unitStats.attackDamage);
            }
            else if (unitName.Contains("maori"))
            {
                DamageStatsManager.Instance.UpdateMaoriDamage(unitStats.attackDamage);
            }
            else if (unitName.Contains("shaman"))
            {
                DamageStatsManager.Instance.UpdateShamanDamage(unitStats.attackDamage);
            }
            else if (unitName.Contains("sarbacane"))
            {
                DamageStatsManager.Instance.UpdateSarbacaneDamage(unitStats.attackDamage);
            }
        }

    }
}
