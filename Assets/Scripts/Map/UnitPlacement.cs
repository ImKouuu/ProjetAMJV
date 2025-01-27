using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class UnitPlacement : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Tilemap benchTilemap;
    [SerializeField] private LayerMask hexLayerMask;
    [SerializeField] private LayerMask unitLayerMask;
    private bool isDragging = false;
    private GameObject draggedUnit;
    private Vector3 initialPosition;

    void Start()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }
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
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, hexLayerMask))
            {
                Vector3 point = hit.point;
                if (hit.collider != null)
                {
                    Vector3 colliderCenter = hit.collider.bounds.center;
                    float unitHeight = draggedUnit.GetComponent<Collider>().bounds.size.y;
                    draggedUnit.transform.position = new Vector3(colliderCenter.x, colliderCenter.y + hit.collider.bounds.extents.y + unitHeight / 2, colliderCenter.z); // Adjust Y position to be on top of the cube
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
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, hexLayerMask))
            {
                Vector3 position = hit.collider.bounds.center;
                Vector3 overlapBoxSize = hit.collider.bounds.size;
                overlapBoxSize.y *= 2; // Augmenter la taille de la boîte de détection sur l'axe Y

                Collider[] colliders = Physics.OverlapBox(position, overlapBoxSize / 2, Quaternion.identity, unitLayerMask);
                if (colliders.Length == 0)
                {
                    // La position est libre, on peut lâcher l'unité
                    draggedUnit.transform.position = new Vector3(position.x, position.y + hit.collider.bounds.extents.y + draggedUnit.GetComponent<Collider>().bounds.size.y / 2, position.z);
                }
                else
                {
                    // La position est occupée, on remet l'unité à sa position initiale
                    draggedUnit.transform.position = initialPosition;
                }
            }
            else
            {
                // Si le raycast ne touche rien, on remet l'unité à sa position initiale
                draggedUnit.transform.position = initialPosition;
            }

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
}