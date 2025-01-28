using UnityEngine;

public class Shot : Attack
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float speed = 10f;

    public override void Start()
    {
        base.Start();
    }
    public override void Launch(Transform target, Animator animator)
    {
        base.Launch(target, animator);
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.linearVelocity = (target.position - transform.position).normalized * speed;
        arrow.transform.LookAt(target);
        arrow.transform.Rotate(90, 0, 0);
    }
}