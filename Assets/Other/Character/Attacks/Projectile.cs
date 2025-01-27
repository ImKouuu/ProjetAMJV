using System.Collections;
using UnityEngine;

[System.Serializable]
public class ProjectileData
{
    public float speed;
    public float damage;
    public bool isAutoGuided;

    [Header("Explosion property")]
    public bool isAnExplosion;
    public float explosionRadius;

    [Header("Other Properties")]
    public GameObject spawnGoOnHit;
    public ParticleSystem particleOnHit;

    [HideInInspector] public Transform target;
    [HideInInspector] public int launcherTeamNumber;
}

public class Projectile : MonoBehaviour
{
    private ProjectileData data = new ProjectileData();

    public void Init(ProjectileData projectileData)
    {
        data = projectileData;

        StartCoroutine(SelfDestroy());
    }

    private void Update()
    {
        if (data.target != null && data.isAutoGuided)
        {
            transform.position += (data.target.position - transform.position).normalized * Time.deltaTime * data.speed;
        }
        else
        {
            transform.position += transform.forward * Time.deltaTime * data.speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!data.isAnExplosion)
        {
            bool isHitten = HitEnnemy(other);

            if (isHitten)
            {
                SpawnParticles();
                SpawnGOonHit();

                Destroy(gameObject);
            }
        }
        //Projectile can't explode on a ally
        if (data.isAnExplosion && !IsAnAlly(other))
        {
            SpawnParticles();
            SpawnGOonHit();

            Collider[] hitten = Physics.OverlapSphere(transform.position, data.explosionRadius);

            foreach (Collider collider in hitten)
            {
                HitEnnemy(collider);
            }

            Destroy(gameObject);
        }
    }

    private bool HitEnnemy(Collider other)
    {
        if (other.tag == "Character")
        {
            Character hitCharacter = other.GetComponent<Character>();

            if (hitCharacter.teamNumber != data.launcherTeamNumber)
            {
                hitCharacter.TakeDamage(data.damage);
                return true;
            }
        }

        return false;
    }

    private bool IsAnAlly(Collider other)
    {
        if(other.tag == "Character")
        {
            return other.GetComponent<Character>().teamNumber == data.launcherTeamNumber;
        }

        return false;
    }

    private void SpawnParticles()
    {
        if (data.particleOnHit != null)
        {
            Instantiate(data.particleOnHit, transform.position, Quaternion.identity);
        }
    }

    private void SpawnGOonHit()
    {
        if (data.spawnGoOnHit != null)
        {
            Instantiate(data.spawnGoOnHit, new Vector3(transform.position.x, data.spawnGoOnHit.transform.position.y,
                transform.position.z), Quaternion.identity);
        }
    }

    IEnumerator SelfDestroy()
    {
        //TODO : Add invisible triggers to delete projectiles
        //TODO : Add a pooling system
        yield return new WaitForSeconds(10f);

        Destroy(gameObject);
    }
}