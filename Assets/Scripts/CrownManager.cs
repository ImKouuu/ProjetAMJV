using UnityEngine;

public class CrownManager : MonoBehaviour
{
    [SerializeField] private GameObject crownPrefab;
    [SerializeField] private float offset;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask layerMask;

    private GameObject crownPosed;
    private GameObject crownHolder = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, float.MaxValue, layerMask))
            {
                Transform _transform = hit.transform;
                crownPosed = GameObject.Find("CrownPosed");
                if (crownPosed != null)
                {
                    Destroy(crownPosed);
                    crownHolder.GetComponent<Crown>().enabled = false;
                    crownHolder = null;
                }
                crownHolder = _transform.gameObject;
                crownHolder.GetComponent<Crown>().enabled = true;

                GameObject newCrown = Instantiate(crownPrefab, _transform.position + Vector3.up * offset, Quaternion.identity, _transform);
                newCrown.name = "CrownPosed";
                crownPosed = newCrown;
            }
        }
    }
}