using System.Collections;
using UnityEngine;

public class PoisonTrigger : MonoBehaviour
{
    [SerializeField] private float dps;
    [SerializeField] private float duration;

    private void Start()
    {
        StartCoroutine(SelfDestroy());
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Character")
        {
            other.GetComponent<Character>().TakeDamage(dps * Time.deltaTime);
        }
    }
}
