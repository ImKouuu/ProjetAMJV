using UnityEngine;

public class RefundManager : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private UnitStats humanStats, elfStats, dwarfStats, trollStats, dragonStats, tikiStats, grosStats, bombeStats, lanceStats, morsureStats, maoriStats, shamanStats, sarbacaneStats;   

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, float.MaxValue, layerMask))
            {
                GameObject unit = hit.transform.gameObject;
                if (unit != null)
                {
                    Refund(unit.name);
                    Destroy(unit);
                }
                else
                {
                    Debug.Log("Unit not found or does not have UnitController component");
                }
            }
        }
    }

    private void Refund(string name)
    {
        if (name.ToLower().Contains("humain"))
        {
            MoneyManager.Instance.AddMoney(humanStats.cost);
        }
        else if (name.ToLower().Contains("elfe"))
        {
            MoneyManager.Instance.AddMoney(elfStats.cost);
        }
        else if (name.ToLower().Contains("nain"))
        {
            MoneyManager.Instance.AddMoney(dwarfStats.cost);
        }
        else if (name.ToLower().Contains("troll"))
        {
            MoneyManager.Instance.AddMoney(trollStats.cost);
        }
        else if (name.ToLower().Contains("dragon"))
        {
            MoneyManager.Instance.AddMoney(dragonStats.cost);
        }
        else if (name.ToLower().Contains("tiki"))
        {
            MoneyManager.Instance.AddMoney(tikiStats.cost);
        }
        else if (name.ToLower().Contains("gros"))
        {
            MoneyManager.Instance.AddMoney(grosStats.cost);
        }
        else if (name.ToLower().Contains("bombe"))
        {
            MoneyManager.Instance.AddMoney(bombeStats.cost);
        }
        else if (name.ToLower().Contains("lance"))
        {
            MoneyManager.Instance.AddMoney(lanceStats.cost);
        }
        else if (name.ToLower().Contains("morsure"))
        {
            MoneyManager.Instance.AddMoney(morsureStats.cost);
        }
        else if (name.ToLower().Contains("maori"))
        {
            MoneyManager.Instance.AddMoney(maoriStats.cost);
        }
        else if (name.ToLower().Contains("shaman"))
        {
            MoneyManager.Instance.AddMoney(shamanStats.cost);
        }
        else if (name.ToLower().Contains("sarbacane"))
        {
            MoneyManager.Instance.AddMoney(sarbacaneStats.cost);
        }
    }
}