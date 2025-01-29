using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsBattleOver { get; private set; } = false;
    public bool IsVictory { get; private set; } = false;

    [SerializeField] private GameObject blueGrass, blueSand, grass, sand;
    [SerializeField] private Tilemap arenaTilemap;
    [SerializeField] private GameObject defeatPanel, victoryPanel;

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    void Start()
    {
        DisableAllUnits();
        MoneyManager.Instance.SaveMoneyBeforeShop();
    }

    void Update()
    {
        if (IsBattleOver)
        {
            Time.timeScale = 0;
            EndBattle(IsVictory);
        }
    }

    public void StartBattle()
    {
        if (!HasCrownPosedUnit())
        {
            Debug.Log("Aucune unité avec 'CrownPosed' n'est présente. La partie ne peut pas être lancée.");
            return;
        }
        MoneyManager.Instance.SaveMoney(); // Sauvegarder l'argent avant de commencer la bataille

        ReplaceBenchTilesWithGrass();
        EnableAllUnitsMovement();
        FindFirstObjectByType<ShopManager>()?.DisableShop();
        FindFirstObjectByType<ModeManager>()?.OnBattleStart();
    }

    private bool HasCrownPosedUnit()
    {
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Unit"))
        {
            if (unit.transform.Find("CrownPosed") != null)
            {
                return true;
            }
        }
        return false;
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

    private void EndBattle(bool isVictory)
    {
        if (isVictory)
        {
            if (victoryPanel != null)
            {
                victoryPanel.SetActive(true);
            }
            else
            {
                Debug.LogError("Victory panel is not assigned in the inspector.");
            }
        }
        else
        {
            if (defeatPanel != null)
            {
                defeatPanel.SetActive(true);
            }
            else
            {
                Debug.LogError("Defeat panel is not assigned in the inspector.");
            }
        }
        IsBattleOver = false;
    }

    public void SetBattleOver(bool isVictory)
    {
        IsBattleOver = true;
        IsVictory = isVictory;
    }
}