using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private WeaponData data;
    private bool canAttack;
    public string enemyTag;

    public void Init(WeaponData weaponData, float attackDuration)
    {
        data = weaponData;
        canAttack = true;

        StartCoroutine(StopAttack(attackDuration));
    }
    public void Start()
    {
        enemyTag = CompareTag("Unit") ? "Enemy" : "Unit";
    }
    IEnumerator StopAttack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canAttack = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == enemyTag && canAttack)
        {
            UnitController hitten = other.GetComponent<UnitController>();

            if(hitten.GetComponent<UnitController>().tag != data.launcherTag)
            {
                hitten.TakeDamage((int)data.damage);
            }
        }
    }
}
