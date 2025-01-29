using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.AI;
using System;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private Button buyButtonHuman, buyButtonElf, buyButtonDwarf, buyButtonTroll, buyButtonDragon, buyButtonTiki, buyButtonGros, buyButtonBombe, buyButtonLance, buyButtonMorsure, buyButtonMaori, buyButtonShaman, buyButtonSarbacane, launchButton;
    [SerializeField] private GameObject humanPrefab, elfPrefab, dwarfPrefab, trollPrefab, dragonPrefab, tikiPrefab, grosPrefab, bombePrefab, lancePrefab, morsurePrefab, maoriPrefab, shamanPrefab, sarbacanePrefab;
    [SerializeField] private UnitStats humanStats, elfStats, dwarfStats, trollStats, dragonStats, tikiStats, grosStats, bombeStats, lanceStats, morsureStats, maoriStats, shamanStats, sarbacaneStats;   
    [SerializeField] private List<Transform> benchSpots = new List<Transform>();
    [SerializeField] private Vector3 checkSize = new Vector3(0.8f, 0.1f, 0.8f); // Taille de la zone de vérification
    [SerializeField] private GameObject benchTilemap;
    [SerializeField] private Canvas shopCanvas; // Référence au canvas du shop
    private bool unlimitedMoney = false;

    void OnEnable()
    {
        buyButtonHuman.onClick.AddListener(() => BuyUnit(humanPrefab, humanStats.cost));
        buyButtonElf.onClick.AddListener(() => BuyUnit(elfPrefab, elfStats.cost));
        buyButtonDwarf.onClick.AddListener(() => BuyUnit(dwarfPrefab, dwarfStats.cost));
        buyButtonTroll.onClick.AddListener(() => BuyUnit(trollPrefab, trollStats.cost));
        buyButtonDragon.onClick.AddListener(() => BuyUnit(dragonPrefab, dragonStats.cost));
        buyButtonTiki.onClick.AddListener(() => BuyUnit(tikiPrefab, tikiStats.cost));
        buyButtonGros.onClick.AddListener(() => BuyUnit(grosPrefab, grosStats.cost));
        buyButtonBombe.onClick.AddListener(() => BuyUnit(bombePrefab, bombeStats.cost));
        buyButtonLance.onClick.AddListener(() => BuyUnit(lancePrefab, lanceStats.cost));
        buyButtonMorsure.onClick.AddListener(() => BuyUnit(morsurePrefab, morsureStats.cost));
        buyButtonMaori.onClick.AddListener(() => BuyUnit(maoriPrefab, maoriStats.cost));
        buyButtonShaman.onClick.AddListener(() => BuyUnit(shamanPrefab, shamanStats.cost));
        buyButtonSarbacane.onClick.AddListener(() => BuyUnit(sarbacanePrefab, sarbacaneStats.cost));
        launchButton.onClick.AddListener(OnBattleStartButtonClicked);

        unlimitedMoney = MoneyManager.Instance.IsMoneyUnlimited();

        UpdateButtonText(buyButtonHuman, "Humain", humanStats.cost);
        UpdateButtonText(buyButtonElf, "Elfe", elfStats.cost);
        UpdateButtonText(buyButtonDwarf, "Nain", dwarfStats.cost);
        UpdateButtonText(buyButtonTroll, "Troll", trollStats.cost);
        UpdateButtonText(buyButtonDragon, "Dragon", dragonStats.cost);
        UpdateButtonText(buyButtonTiki, "Tiki", tikiStats.cost);
        UpdateButtonText(buyButtonGros, "Gros", grosStats.cost);
        UpdateButtonText(buyButtonBombe, "Bombe", bombeStats.cost);
        UpdateButtonText(buyButtonLance, "Lance", lanceStats.cost);
        UpdateButtonText(buyButtonMorsure, "Morsure", morsureStats.cost);
        UpdateButtonText(buyButtonMaori, "Maori", maoriStats.cost);
        UpdateButtonText(buyButtonShaman, "Shaman", shamanStats.cost);
        UpdateButtonText(buyButtonSarbacane, "Sarbacane", sarbacaneStats.cost);
    }

    void OnDisable()
    {
        buyButtonHuman.onClick.RemoveListener(() => BuyUnit(humanPrefab, humanStats.cost));
        buyButtonElf.onClick.RemoveListener(() => BuyUnit(elfPrefab, elfStats.cost));
        buyButtonDwarf.onClick.RemoveListener(() => BuyUnit(dwarfPrefab, dwarfStats.cost));
        buyButtonTroll.onClick.RemoveListener(() => BuyUnit(trollPrefab, trollStats.cost));
        buyButtonDragon.onClick.RemoveListener(() => BuyUnit(dragonPrefab, dragonStats.cost));
        buyButtonTiki.onClick.RemoveListener(() => BuyUnit(tikiPrefab, tikiStats.cost));
        buyButtonGros.onClick.RemoveListener(() => BuyUnit(grosPrefab, grosStats.cost));
        buyButtonBombe.onClick.RemoveListener(() => BuyUnit(bombePrefab, bombeStats.cost));
        buyButtonLance.onClick.RemoveListener(() => BuyUnit(lancePrefab, lanceStats.cost));
        buyButtonMorsure.onClick.RemoveListener(() => BuyUnit(morsurePrefab, morsureStats.cost));
        buyButtonMaori.onClick.RemoveListener(() => BuyUnit(maoriPrefab, maoriStats.cost));
        buyButtonShaman.onClick.RemoveListener(() => BuyUnit(shamanPrefab, shamanStats.cost));
        buyButtonSarbacane.onClick.RemoveListener(() => BuyUnit(sarbacanePrefab, sarbacaneStats.cost));

        launchButton.onClick.RemoveListener(OnBattleStartButtonClicked);
    }

    private void UpdateButtonText(Button button, String name, int cost)
    {
        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
        if (buttonText != null)
        {
            buttonText.text = $"Acheter {name} ({cost})";
        }
    }

    public void OnBattleStartButtonClicked()
    {
        GameManager.Instance.StartBattle();

    }

    public void DisableShop()
    {
        buyButtonHuman.interactable = false;
        buyButtonElf.interactable = false;
        buyButtonDwarf.interactable = false;
        buyButtonTroll.interactable = false;
        buyButtonDragon.interactable = false;
        buyButtonTiki.interactable = false;
        buyButtonGros.interactable = false;
        buyButtonBombe.interactable = false;
        buyButtonLance.interactable = false;
        buyButtonMorsure.interactable = false;
        buyButtonMaori.interactable = false;
        buyButtonShaman.interactable = false;
        buyButtonSarbacane.interactable = false;
        
        benchTilemap.SetActive(false);
        shopCanvas.gameObject.SetActive(false);
        DamageStatsManager.Instance.ShowDamageStats();
    }

    public void EnableShop()
    {
        buyButtonHuman.interactable = true;
        buyButtonElf.interactable = true;
        buyButtonDwarf.interactable = true;
        buyButtonTroll.interactable = true;
        buyButtonDragon.interactable = true;
        buyButtonTiki.interactable = true;
        buyButtonGros.interactable = true;
        buyButtonBombe.interactable = true;
        buyButtonLance.interactable = true;
        buyButtonMorsure.interactable = true;
        buyButtonMaori.interactable = true;
        buyButtonShaman.interactable = true;
        buyButtonSarbacane.interactable = true;

        benchTilemap.SetActive(true);
        shopCanvas.gameObject.SetActive(true); // Réactiver le canvas du shop
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
        newUnit.name = prefab.name;
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
            moneyText.text = $"Argent : {(unlimitedMoney ? "∞" : MoneyManager.Instance.GetMoney())}";
        }
    }
}