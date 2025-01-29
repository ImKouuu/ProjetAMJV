using System.Collections;
using UnityEngine;

public class Healing : Attack
{
    [SerializeField] private ParticleSystem healParticle;
    [SerializeField] private float heal;
    [SerializeField] private float waitBeforeHeal;
    private string launcherTag;
    public override void Start()
    {
        base.Start();
        unitController = GetComponent<UnitController>();
    }

    public override void Launch(Transform target, Animator animator)
    {
        base.Launch(target, animator);

        healParticle.Play();
        StartCoroutine(HealDelayer(transform.GetComponent<UnitController>()));
    }

    private IEnumerator HealDelayer(UnitController launcher)
    {
        yield return new WaitForSeconds(waitBeforeHeal);

        launcher.TakeDamage(-(int)heal);
    }
}
