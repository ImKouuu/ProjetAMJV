using UnityEngine;

public class Fireball : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            UnitController enemyController = collision.gameObject.GetComponent<UnitController>();
            if (enemyController != null)
            {
                UnitStats unitStats = GetComponent<UnitStats>();
                enemyController.TakeDamage(unitStats.specialAttackDamage);
            }
        }
        Destroy(gameObject);
    }
}
