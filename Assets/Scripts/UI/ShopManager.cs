using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private Button buyHumanButton;
    [SerializeField] private GameObject mobPrefab; // Référence au prefab du mob
    [SerializeField] private Tilemap benchTilemap; // Référence à la tilemap du banc
    [SerializeField] private TileBase benchTile; // Référence à la tile du banc
    [SerializeField] private LayerMask unitLayerMask; // LayerMask pour les unités

    void Start()
    {
        buyHumanButton.onClick.AddListener(BuyHuman);
    }

    void Update()
    {
        moneyText.text = "Money: " + MoneyManager.Instance.GetMoney();
    }

    private void BuyHuman()
    {
        Vector3Int? freeTilePosition = FindFreeTileOnBench();
        if (freeTilePosition.HasValue)
        {
            if (MoneyManager.Instance.GetMoney() >= 10) // Vérifiez si le joueur a assez d'argent
            {
                MoneyManager.Instance.SpendMoney(10);
                Vector3 worldPosition = benchTilemap.GetCellCenterWorld(freeTilePosition.Value);
                Instantiate(mobPrefab, worldPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Not enough money to buy a human!");
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
                Vector3 worldPosition = benchTilemap.CellToWorld(position) + benchTilemap.tileAnchor;
                Ray ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(worldPosition));
                if (!Physics.Raycast(ray, Mathf.Infinity, unitLayerMask))
                {
                    return position;
                }
            }
        }
        return null;
    }
}