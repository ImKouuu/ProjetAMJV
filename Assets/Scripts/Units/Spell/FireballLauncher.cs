using UnityEngine;

public class FireballLauncher : Spell
{
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private float speed = 10f;

    public override void CastSpell(Transform target, Animator animator)
    {
        base.CastSpell(target, animator);
        GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        rb.linearVelocity = (target.position - transform.position).normalized * speed;
    }


}