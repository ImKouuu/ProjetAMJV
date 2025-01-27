using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsBattleActive { get; private set; } = false;

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
        EnableAllUnitsMovement();
        FindFirstObjectByType<ShopManager>()?.DisableShop(); // Désactive la boutique
    }

    private void DisableAllUnits()
    {
        foreach (UnitMovement unit in FindObjectsByType<UnitMovement>(FindObjectsSortMode.None))
        {
            DisableNavMeshAgent(unit);
            unit.enabled = false; // Désactiver le script de mouvement
            Attack attackScript = unit.GetComponent<Attack>();
            if (attackScript != null)
            {
                attackScript.enabled = false; // Désactiver le script d'attaque
            }
        }
    }

    private void EnableAllUnitsMovement()
    {
        foreach (UnitMovement unit in FindObjectsByType<UnitMovement>(FindObjectsSortMode.None))
        {
            EnableNavMeshAgent(unit);
            unit.enabled = true; // Activer le script de mouvement
            Attack attackScript = unit.GetComponent<Attack>();
            if (attackScript != null)
            {
                attackScript.enabled = true; // Activer le script d'attaque
            }
        }
    }

    private void DisableNavMeshAgent(UnitMovement unit)
    {
        NavMeshAgent agent = unit.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.enabled = false;
        }
    }

    private void EnableNavMeshAgent(UnitMovement unit)
    {
        NavMeshAgent agent = unit.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.enabled = true;
        }
    }
}