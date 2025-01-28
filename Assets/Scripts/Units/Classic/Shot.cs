using UnityEngine;

public class Shot : Attack
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float speed = 10f;

    public new void Launch(Transform target, Animator animator)
    {
        base.Launch(target, animator);
        Debug.Log("Shot launched");
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.linearVelocity = (target.position - transform.position).normalized * speed;
    }
}