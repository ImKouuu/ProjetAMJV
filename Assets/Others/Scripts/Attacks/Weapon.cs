using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private WeaponData data;
    private bool canAttack;

    public void Init(WeaponData weaponData, float attackDuration)
    {
        data = weaponData;
        canAttack = true;

        StartCoroutine(StopAttack(attackDuration));
    }

    IEnumerator StopAttack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canAttack = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Character" && canAttack)
        {
            Character hitten = other.GetComponent<Character>();

            if(hitten.teamNumber != data.launcherTeamNumber)
            {
                hitten.TakeDamage(data.damage);
            }
        }
    }
}
