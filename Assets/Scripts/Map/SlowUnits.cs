using UnityEngine;
using UnityEngine.AI;

public class SlowZone : MonoBehaviour {
    [SerializeField] private float slowFactor = 0.5f;

    private void OnTriggerEnter(Collider other) {
        NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
        if (agent != null) {
            agent.speed *= slowFactor;
        }
    }

    private void OnTriggerExit(Collider other) {
        NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
        if (agent != null) {
            agent.speed /= slowFactor;
        }
    }
}