using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class MouseHoverHexagon : MonoBehaviour
{
    [SerializeField] private Tile hexHighlightTile;
    [SerializeField] private Camera _camera;
    private Tilemap tilemap;
    [SerializeField] private LayerMask _layerMask;
    private TileBase previousTile; // La tuile précédente sous la souris
    private Vector3Int currentPosition; // Position actuelle sous la souris
    private Vector3Int previousPosition; // Position précédente sous la souris

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        if (_camera == null)
        {
            _camera = Camera.main;
        }
        previousPosition = new Vector3Int(-1, -1, -1);
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = _camera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, _layerMask))
        {
            Vector3 point = hit.point;
            currentPosition = tilemap.WorldToCell(point);

            TileBase currentTile = tilemap.GetTile(currentPosition);

            if (currentTile != null)
            {
                if (previousPosition != currentPosition)
                {
                    if (previousTile != null)
                    {
                        tilemap.SetTile(previousPosition, previousTile);
                    }
                    previousTile = currentTile;
                    previousPosition = currentPosition;
                }

                if (currentTile != hexHighlightTile)
                {
                    tilemap.SetTile(currentPosition, hexHighlightTile);
                }
            }
            else {
                if (previousTile != null)
                {
                    tilemap.SetTile(previousPosition, previousTile);
                    previousTile = null;
                    previousPosition = new Vector3Int(-1, -1, -1);
                }
            }
        }
    }
}