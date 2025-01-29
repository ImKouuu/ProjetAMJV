using System.Collections;
using UnityEngine;

public class ProjectilesThrower : Attack
{
    [Header("Thrower properties")]
    [SerializeField] private GameObject projectiles;
    [SerializeField] private Transform throwPosition;
    [SerializeField] private float waitingTime;

    [Header("Projectile property")]
    [SerializeField] private ProjectileData projectileData;
    private string launcherTag;
    public override void Start()
    {
        base.Start();
        unitController = GetComponent<UnitController>();
    }
    public override void Launch(Transform target, Animator animator)
    {
        base.Launch(target, animator);
        launcherTag = unitController.tag;
        projectileData.target = target;
        projectileData.launcherTag = launcherTag;

        StartCoroutine(LaunchProjectile());
    }

    IEnumerator LaunchProjectile()
    {
        yield return new WaitForSeconds(waitingTime);

        Projectile[] projectilesArray = projectiles.GetComponentsInChildren<Projectile>(); 

        //Initialize all projectiles in the prefab (useful for multiples projectiles in one prefab)
        foreach (Projectile projectile in projectilesArray)
        {
            Projectile throwed = Instantiate(projectile, throwPosition.position + projectile.transform.position, 
                throwPosition.rotation * projectile.transform.rotation);
            throwed.Init(projectileData);
        }
    }
}