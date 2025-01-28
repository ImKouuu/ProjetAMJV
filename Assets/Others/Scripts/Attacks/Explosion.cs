using System.Collections;
using UnityEngine;

public class Explosion : Attack
{
    [SerializeField] private ParticleSystem blowParticle;
    [SerializeField] private float damage;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float waitBeforeExplode;

    private int launcherTeamNumber;

    public override void Launch(Character launcher, Transform target, Animator animator)
    {
        base.Launch(launcher, target, animator);
        launcherTeamNumber = launcher.teamNumber;

        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(waitBeforeExplode);

        blowParticle.Play();

        Collider[] hitten = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider col in hitten)
        {
            if (col.tag == "Character")
            {
                Character hitCharacter = col.GetComponent<Character>();

                if (hitCharacter.teamNumber != launcherTeamNumber)
                {
                    hitCharacter.TakeDamage(damage);
                }
            }
        }
    }
}
