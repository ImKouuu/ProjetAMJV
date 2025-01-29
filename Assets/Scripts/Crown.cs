using UnityEngine;

public class Crown : MonoBehaviour
{
    private UnitController unitController;

    private void Start()
    {
        unitController = GetComponent<UnitController>();
        if (unitController == null)
        {
            Debug.LogError("UnitController is missing on " + gameObject.name);
            enabled = false;
        }
    }

    private void Update()
    {
        if (unitController != null)
        {
            UpdateTargetsBasedOnCrown(unitController);
        }
    }

    private void UpdateTargetsBasedOnCrown(UnitController unitController)
    {
        string crownTag = unitController.CompareTag("Unit") ? "Unit" : "Enemy";
        string oppositeTag = crownTag == "Unit" ? "Enemy" : "Unit";

        foreach (UnitMovement unit in FindObjectsByType<UnitMovement>(FindObjectsSortMode.None))
        {
            if (unit.CompareTag(crownTag))
            {
                if (unit.GetCurrentMode() == UnitMovement.MovementMode.Defensive)
                {
                    unit.SetTarget(transform);
                }
            }
            else if (unit.CompareTag(oppositeTag))
            {
                if (unit.GetCurrentMode() == UnitMovement.MovementMode.Offensive)
                {
                    unit.SetTarget(transform);
                }
            }
        }
    }
}