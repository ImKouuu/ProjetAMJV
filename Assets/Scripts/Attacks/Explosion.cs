using System.Collections;
using UnityEngine;

public class Explosion : Attack
{
    [SerializeField] private ParticleSystem blowParticle;
    [SerializeField] private float damage;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float waitBeforeExplode;
    

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

        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(waitBeforeExplode);

        blowParticle.Play();

        Collider[] hitten = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider col in hitten)
        {
            if (col.tag == enemyTag)
            {
                UnitController hitunitController = col.GetComponent<UnitController>();
                
                if (hitunitController.GetComponent<UnitController>().tag != launcherTag)
                {
                    hitunitController.TakeDamage((int)damage);
                }
            }
        }
    }
}
