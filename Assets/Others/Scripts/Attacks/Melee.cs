using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData
{
    public Weapon weapon;
    public float damage;

    [HideInInspector] public int launcherTeamNumber;
}

public class Melee : Attack
{
    [SerializeField] private List<WeaponData> weapons;

    public override void Launch(Character launcher, Transform target, Animator animator)
    {
        base.Launch(launcher, target, animator);

        foreach (WeaponData weaponData in weapons)
        {
            weaponData.launcherTeamNumber = launcher.teamNumber;
            weaponData.weapon.Init(weaponData, base.GetAnimationClipTime());
        }
    }
}
