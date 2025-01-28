using UnityEngine;

public class Crown : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        UnitController unitController = other.GetComponent<UnitController>();

        if (unitController != null)
        {
            UpdateTargetsBasedOnCrown(unitController);
        }
    }

    private void UpdateTargetsBasedOnCrown(UnitController unitController)
    {
        string crownTag = unitController.CompareTag("Unit") ? "Unit" : "Enemy";

        foreach (UnitMovement unit in FindObjectsByType<UnitMovement>(FindObjectsSortMode.None))
        {
            if (unit.CompareTag(crownTag))
            {
                if (unit.GetCurrentMode() == UnitMovement.MovementMode.Offensive)
                {
                    unit.SetTarget(transform);
                }
            }
            else
            {
                if (unit.GetCurrentMode() == UnitMovement.MovementMode.Defensive)
                {
                    unit.SetTarget(transform);
                }
            }
        }
    }
}
