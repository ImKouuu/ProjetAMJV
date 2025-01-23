using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private Button buyHumanButton;
    [SerializeField] private Button buyElfButton; // Bouton pour acheter des elfes
    [SerializeField] private GameObject humanPrefab; // Référence au prefab du mob humain
    [SerializeField] private GameObject elfPrefab; // Référence au prefab du mob elfe
    [SerializeField] private Tilemap benchTilemap; // Référence à la tilemap du banc
    [SerializeField] private TileBase benchTile; // Référence à la tile du banc
    [SerializeField] private LayerMask unitLayerMask; // LayerMask pour les unités
    [SerializeField] private float spawnHeightOffset = 1.0f; // Décalage vertical pour le spawn
    [SerializeField] private int humanCost = 10; // Coût pour acheter un humain
    [SerializeField] private int elfCost = 15; // Coût pour acheter un elfe

    void Start()
    {
        buyHumanButton.onClick.AddListener(() => BuyUnit(humanPrefab, humanCost));
        buyElfButton.onClick.AddListener(() => BuyUnit(elfPrefab, elfCost));
    }

    void Update()
    {
        moneyText.text = "Money: " + MoneyManager.Instance.GetMoney();
    }

    private void BuyUnit(GameObject unitPrefab, int cost)
    {
        Vector3Int? freeTilePosition = FindFreeTileOnBench();
        if (freeTilePosition.HasValue)
        {
            if (MoneyManager.Instance.GetMoney() >= cost) // Vérifiez si le joueur a assez d'argent
            {
                MoneyManager.Instance.SpendMoney(cost);
                Vector3 worldPosition = benchTilemap.GetCellCenterWorld(freeTilePosition.Value);
                worldPosition.y += spawnHeightOffset; // Ajouter le décalage vertical
                Instantiate(unitPrefab, worldPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Not enough money to buy the unit!");
            }
        }
        else
        {
            Debug.LogWarning("No free tile available on the bench!");
        }
    }

    private Vector3Int? FindFreeTileOnBench()
    {
        foreach (Vector3Int position in benchTilemap.cellBounds.allPositionsWithin)
        {
            if (benchTilemap.GetTile(position) == benchTile)
            {
                Vector3 worldPosition = benchTilemap.GetCellCenterWorld(position);
                Collider[] colliders = Physics.OverlapBox(worldPosition, benchTilemap.cellSize / 2, Quaternion.identity, unitLayerMask);
                if (colliders.Length == 0)
                {
                    return position;
                }
            }
        }
        return null;
    }
}