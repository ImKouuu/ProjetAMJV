using System.Collections;
using UnityEngine;

public class Healing : Attack
{
    [SerializeField] private ParticleSystem healParticle;
    [SerializeField] private float heal;
    [SerializeField] private float waitBeforeHeal;

    public override void Launch(Character launcher, Transform target, Animator animator)
    {
        base.Launch(launcher, target, animator);

        healParticle.Play();
        StartCoroutine(HealDelayer(launcher));
    }

    private IEnumerator HealDelayer(Character launcher)
    {
        yield return new WaitForSeconds(waitBeforeHeal);

        launcher.TakeDamage(-heal);
    }
}
