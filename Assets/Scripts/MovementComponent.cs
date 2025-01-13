using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] private float speed;

    void Update()
    {
        // Simple mouvement de test
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
