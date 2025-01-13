using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class UnitPlacement : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private Tilemap hexTilemap;
    [SerializeField] private Tilemap benchTilemap;
    [SerializeField] private LayerMask hexLayerMask;
    [SerializeField] private LayerMask benchLayerMask;
    [SerializeField] private LayerMask unitLayerMask;
    private TileBase previousTile; // La tuile précédente sous la souris
    private Vector3Int currentPosition; // Position actuelle sous la souris
    private Vector3Int previousPosition; // Position précédente sous la souris
    private bool isDragging = false;
    private GameObject draggedUnit;
    private Vector3 initialPosition;

    void Start()
    {
        hexTilemap = GetComponent<Tilemap>();
        if (_camera == null)
        {
            _camera = Camera.main;
        }
        previousPosition = new Vector3Int(-1, -1, -1);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }
        if (Input.GetMouseButtonUp(0))
        {
            OnMouseUp();
        }
        if (isDragging && draggedUnit != null)
        {
            initialPosition = draggedUnit.transform.position;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, hexLayerMask) || Physics.Raycast(ray, out hit, float.MaxValue, benchLayerMask))
            {
                Vector3 point = hit.point;
                if (hexTilemap.GetTile(hexTilemap.WorldToCell(point)) != null)
                {
                    Vector3Int cellPosition = hexTilemap.WorldToCell(point);
                    Vector3 worldPosition = hexTilemap.GetCellCenterWorld(cellPosition);
                    draggedUnit.transform.position = new Vector3(worldPosition.x, draggedUnit.transform.position.y, worldPosition.z);
                }
                else if (benchTilemap.GetTile(benchTilemap.WorldToCell(point)) != null)
                {
                    Vector3Int cellPosition = benchTilemap.WorldToCell(point);
                    Vector3 worldPosition = benchTilemap.GetCellCenterWorld(cellPosition);
                    draggedUnit.transform.position = new Vector3(worldPosition.x, draggedUnit.transform.position.y, worldPosition.z);
                }
                else
                {
                    draggedUnit.transform.position = initialPosition;
                }
            }
        }
    }

    void OnMouseDown()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, unitLayerMask))
        {
            Debug.Log("Hit " + hit.collider.name);
            if (hit.collider.CompareTag("Unit"))
            {
                isDragging = true;
                draggedUnit = hit.collider.gameObject;
                initialPosition = draggedUnit.transform.position;
                Rigidbody rb = draggedUnit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true; // Désactiver la physique
                }
                Collider collider = draggedUnit.GetComponent<Collider>();
                if (collider != null)
                {
                    collider.isTrigger = true; // Permettre de traverser les murs
                }
            }
        }
    }

    void OnMouseUp()
    {
        if (draggedUnit != null)
        {
            Rigidbody rb = draggedUnit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Réactiver la physique
            }
            Collider collider = draggedUnit.GetComponent<Collider>();
            if (collider != null)
            {
                collider.isTrigger = false; // Désactiver le mode trigger
            }
            draggedUnit = null;
        }
        isDragging = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = _camera.nearClipPlane;
        return _camera.ScreenToWorldPoint(mouseScreenPosition);
    }
}