using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsBattleActive { get; private set; } = false;

    [SerializeField] private GameObject blueGrass, blueSand, grass, sand;
    [SerializeField] private Tilemap arenaTilemap;

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    void Start()
    {
        DisableAllUnits();
    }

    public void StartBattle()
    {
        IsBattleActive = true;
        ReplaceBenchTilesWithGrass();
        EnableAllUnitsMovement();
        FindFirstObjectByType<ShopManager>()?.DisableShop();
    }

    private void DisableAllUnits()
    {
        foreach (UnitMovement unit in FindObjectsByType<UnitMovement>(FindObjectsSortMode.None))
        {
            DisableUnit(unit.gameObject);
        }
    }

    public void DisableUnit(GameObject unit)
    {
        NavMeshAgent agent = unit.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.enabled = false;
        }

        UnitMovement unitMovement = unit.GetComponent<UnitMovement>();
        if (unitMovement != null)
        {
            unitMovement.enabled = false;
        }

        Attack attackScript = unit.GetComponent<Attack>();
        if (attackScript != null)
        {
            attackScript.enabled = false;
        }
    }

    private void EnableAllUnitsMovement()
    {
        foreach (UnitMovement unit in FindObjectsByType<UnitMovement>(FindObjectsSortMode.None))
        {
            EnableUnit(unit.gameObject);
        }
    }

    public void EnableUnit(GameObject unit)
    {
        NavMeshAgent agent = unit.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.enabled = true;
        }

        UnitMovement unitMovement = unit.GetComponent<UnitMovement>();
        if (unitMovement != null)
        {
            unitMovement.enabled = true;
        }

        Attack attackScript = unit.GetComponent<Attack>();
        if (attackScript != null)
        {
            attackScript.enabled = true;
        }
    }

    private void ReplaceBenchTilesWithGrass()
    {
        foreach (Transform child in arenaTilemap.transform)
        {
            if (child.gameObject.name == blueGrass.name)
            {
                Vector3 position = child.position;
                Destroy(child.gameObject);
                Instantiate(grass, position, Quaternion.identity, arenaTilemap.transform);
            }
            else if (child.gameObject.name == blueSand.name)
            {
                Vector3 position = child.position;
                Destroy(child.gameObject);
                Instantiate(sand, position, Quaternion.identity, arenaTilemap.transform);
            }
        }
    }
}