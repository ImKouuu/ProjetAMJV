using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData
{
    public Weapon weapon;
    public float damage;

    [HideInInspector] public string launcherTag;
}

public class Melee : Attack
{
    [SerializeField] private List<WeaponData> weapons;
    private string launcherTag;
    public override void Start()
    {
        base.Start();
        unitController = GetComponent<UnitController>();
    }

    public override void Launch(Transform target, Animator animator)
    {
        base.Launch(target, animator);
        launcherTag = unitController.tag;
        foreach (WeaponData weaponData in weapons)
        {
            weaponData.launcherTag = launcherTag;
            weaponData.weapon.Init(weaponData, animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }
}
