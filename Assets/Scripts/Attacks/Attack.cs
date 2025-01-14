using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private bool isSpell;
    [SerializeField] private AnimationClip animationClip;
    [SerializeField] private float cooldown;
    //[SerializeField] public float range;
    [SerializeField] private UnitStats unitStats;

    public virtual void Launch(Transform target, Animator animator,bool isSpell)
    {
        if(isSpell)
        {
            animator.Play("Spell");
        }
        else
        {
            animator.Play("Attack");
        }
    }

    public float GetCooldown()
    {
        return animationClip.length + cooldown;
    }
}