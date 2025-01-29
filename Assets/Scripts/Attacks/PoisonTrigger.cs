using System.Collections;
using UnityEngine;

public class PoisonTrigger : MonoBehaviour
{
    [SerializeField] private float dps;
    [SerializeField] private float duration;
    public string enemyTag;

    private void Start()
    {
        StartCoroutine(SelfDestroy());
        enemyTag = CompareTag("Unit") ? "Enemy" : "Unit";
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == enemyTag)
        {
            other.GetComponent<UnitController>().TakeDamage((int)(dps * Time.deltaTime));
        }
    }
}
