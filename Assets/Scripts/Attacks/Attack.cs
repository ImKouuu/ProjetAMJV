using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private bool isSpell;
    [SerializeField] private AnimationClip attack;
    [SerializeField] private AnimationClip spell;
    private AnimationClip animationClip;

    public virtual void Launch(Transform target, Animator animator,bool isSpell)
    {
        if(isSpell)
        {
            animator.Play("Spell");
            animationClip = spell;
        }
        else
        {
            animator.Play("Attack");
            animationClip = attack;
        }
    }


}