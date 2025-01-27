using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.AI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private Button buyButtonHuman, buyButtonElf, launchButton;
    [SerializeField] private GameObject humanPrefab;
    [SerializeField] private GameObject elfPrefab;
    [SerializeField] private int humanCost = 10;
    [SerializeField] private int elfCost = 15;
    [SerializeField] private List<Transform> benchSpots = new List<Transform>();
    [SerializeField] private Vector3 checkSize = new Vector3(0.8f, 0.1f, 0.8f); // Taille de la zone de vérification
    [SerializeField] private GameObject benchTilemap;
    private bool unlimitedMoney = false;

    void Start()
    {
        buyButtonHuman.onClick.AddListener(() => BuyUnit(humanPrefab, humanCost));
        buyButtonElf.onClick.AddListener(() => BuyUnit(elfPrefab, elfCost));
        launchButton.onClick.AddListener(OnBattleStartButtonClicked);
        
        Debug.Log($"Spots initialisés : {benchSpots.Count}");

        unlimitedMoney = MoneyManager.Instance.IsMoneyUnlimited();
    }

    public void OnBattleStartButtonClicked()
    {
        GameManager.Instance.StartBattle();
    }

    public void DisableShop()
    {
        buyButtonHuman.interactable = false;
        buyButtonElf.interactable = false;
        benchTilemap.SetActive(false);
    }

    private void BuyUnit(GameObject unitPrefab, int cost)
    {
        if (MoneyManager.Instance.GetMoney() < cost)
        {
            Debug.Log("Pas assez d'argent !");
            return;
        }

        Transform freeSpot = FindFreeBenchSpot();
        
        if (freeSpot != null)
        {
            if (!unlimitedMoney)
            {
                MoneyManager.Instance.SpendMoney(cost);
            }
            PlaceUnit(unitPrefab, freeSpot);
        }
        else
        {
            Debug.Log("Banc complet !");
        }
    }

    private Transform FindFreeBenchSpot()
    {
        foreach (Transform spot in benchSpots)
        {
            // Vérification précise avec OverlapBox
            Collider[] colliders = Physics.OverlapBox(
                spot.position + Vector3.up * 0.05f, // Léger offset vertical
                checkSize / 2,
                Quaternion.identity
            );

            bool occupied = false;
            foreach (Collider col in colliders)
            {
                if (col.CompareTag("Unit"))
                {
                    occupied = true;
                    break;
                }
            }

            // Debug visuel
            Debug.DrawLine(spot.position - checkSize/2, spot.position + checkSize/2, 
                         occupied ? Color.red : Color.green, 0.5f);

            if (!occupied) return spot;
        }
        return null;
    }

    private void PlaceUnit(GameObject prefab, Transform spot)
    {
        // Placement précis sur le cube
        Vector3 spawnPosition = spot.position;
        if (spot.TryGetComponent<Collider>(out Collider col))
        {
            spawnPosition.y += col.bounds.extents.y; // Haut du cube
        }
    
        GameObject newUnit = Instantiate(prefab, spawnPosition, Quaternion.identity);
        NavMeshAgent agent = newUnit.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.enabled = false;
        }
    
        // Désactiver les scripts de mouvement et d'attaque
        UnitMovement unitMovement = newUnit.GetComponent<UnitMovement>();
        if (unitMovement != null)
        {
            unitMovement.enabled = false;
        }
    
        Attack attackScript = newUnit.GetComponent<Attack>();
        if (attackScript != null)
        {
            attackScript.enabled = false;
        }
    
        // Alignement final
        if (newUnit.TryGetComponent<Collider>(out Collider unitCol))
        {
            newUnit.transform.position += Vector3.up * unitCol.bounds.extents.y;
        }
    }

    void Update()
    {
        if (MoneyManager.Instance != null)
        {
            moneyText.text = $"Money: {(unlimitedMoney ? "∞" : MoneyManager.Instance.GetMoney())}";
        }
    }
}