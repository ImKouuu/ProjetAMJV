using UnityEngine;

public class MeleeAttack : Attack
{
    public override void Launch(Transform target, Animator animator, bool isSpell)
    {
        base.Launch(target, animator, isSpell);

        Debug.Log("Test Attack");
    }
}
