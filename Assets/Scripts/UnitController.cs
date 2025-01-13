using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private float speed;

    private DragAndDropController dragAndDropController;

    void Start()
    {
        dragAndDropController = GetComponent<DragAndDropController>();
        if (dragAndDropController == null)
        {
            dragAndDropController = gameObject.AddComponent<DragAndDropController>();
        }
    }
}