using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ModeManager : MonoBehaviour
{
    [SerializeField] private GameObject modeCanvasPrefab;
    [SerializeField] private float modeHeightOffset, fontSize;
    [SerializeField] private LayerMask unitLayerMask;
    [SerializeField] private Camera cam;
    private List<UnitMovement.MovementMode> modes = new List<UnitMovement.MovementMode> { UnitMovement.MovementMode.Offensive, UnitMovement.MovementMode.Defensive, UnitMovement.MovementMode.Neutral };
    private bool battleStarted = false;
    private List<GameObject> unitsAndEnemies;

    void Start()
    {
        CreateModeTexts();
    }

    void Update()
    {
        UpdateList();
        if (Input.GetKeyDown(KeyCode.M))
        {
            ChangeMode();
        }
        if (!battleStarted)
        {
            CreateModeTexts();
        }
        checkIfCrowned();
    }

    private void UpdateList()
    {
        unitsAndEnemies = new List<GameObject>();
        unitsAndEnemies.AddRange(GameObject.FindGameObjectsWithTag("Unit"));
        unitsAndEnemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    private void checkIfCrowned()
    {
        foreach (GameObject unit in unitsAndEnemies)
        {
            Crown crown = unit.GetComponent<Crown>();
            if (crown != null && crown.enabled)
            {
                Canvas modeCanvas = unit.GetComponentInChildren<Canvas>();
                if (modeCanvas != null && modeCanvas.gameObject.name == "ModeCanvas")
                {
                    modeCanvas.gameObject.SetActive(false);
                }
                UnitMovement unitMovement = unit.GetComponent<UnitMovement>();
                if (unitMovement != null)
                {
                    unitMovement.SetMovementMode(UnitMovement.MovementMode.Neutral);
                }
            }
        }
    }

    public void CreateModeTexts()
    {
        foreach (GameObject unit in unitsAndEnemies)
        {
            // Vérifiez si le canvas existe déjà
            Canvas modeCanvas = null;
            Canvas[] existingCanvases = unit.GetComponentsInChildren<Canvas>();
            foreach (Canvas existingCanvas in existingCanvases)
            {
                if (existingCanvas.gameObject.name == "ModeCanvas")
                {
                    modeCanvas = existingCanvas;
                    break;
                }
            }

            // Si le canvas n'existe pas, créez-le
            if (modeCanvas == null)
            {
                GameObject canvasObject = Instantiate(modeCanvasPrefab, unit.transform);
                canvasObject.name = "ModeCanvas";
                modeCanvas = canvasObject.GetComponent<Canvas>();
                modeCanvas.transform.localPosition = new Vector3(0, modeHeightOffset, 0);
            }

            // Mettez à jour le texte du mode
            TMP_Text modeText = modeCanvas.transform.GetComponentInChildren<TMP_Text>();
            modeText.fontSize = fontSize; // Appliquer la taille du texte
            UnitMovement unitMovement = unit.GetComponent<UnitMovement>();
            if (unitMovement != null)
            {
                modeText.text = TranslateMode(unitMovement.GetCurrentMode());
            }
        }
    }

    private void ChangeMode()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, unitLayerMask))
        {
            GameObject unit = hit.transform.gameObject;
            UnitMovement unitMovement = unit.GetComponent<UnitMovement>();
            if (unitMovement != null)
            {
                int currentModeIndex = modes.IndexOf(unitMovement.GetCurrentMode());
                int nextModeIndex = (currentModeIndex + 1) % modes.Count;
                unitMovement.SetMovementMode(modes[nextModeIndex]);

                TMP_Text modeText = unit.GetComponentInChildren<TMP_Text>();
                if (modeText != null)
                {
                    modeText.text = TranslateMode(unitMovement.GetCurrentMode());
                }
            }
        }
    }

    public void OnBattleStart()
    {
        battleStarted = true;
        foreach (GameObject unit in unitsAndEnemies)
        {
            Canvas modeCanvas = unit.GetComponentInChildren<Canvas>();
            if (modeCanvas != null && modeCanvas.gameObject.name == "ModeCanvas")
            {
                modeCanvas.gameObject.SetActive(false);
            }
        }
    }

    void LateUpdate()
    {
        foreach (GameObject unit in unitsAndEnemies)
        {
            Canvas modeCanvas = unit.GetComponentInChildren<Canvas>();
            if (modeCanvas != null && modeCanvas.gameObject.name == "ModeCanvas")
            {
                Vector3 cameraPosition = cam.transform.position;
                Vector3 direction = (modeCanvas.transform.position - cameraPosition).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                modeCanvas.transform.rotation = lookRotation;
                modeCanvas.transform.Rotate(0, 180, 0); // Inverser l'axe Y pour rendre le texte en miroir
                modeCanvas.transform.localScale = new Vector3(-1, 1, 1); // Inverser l'axe X pour corriger le texte en miroir
            }
        }
    }

    private string TranslateMode(UnitMovement.MovementMode mode)
    {
        switch (mode)
        {
            case UnitMovement.MovementMode.Offensive:
                return "Offensif";
            case UnitMovement.MovementMode.Defensive:
                return "Défensif";
            case UnitMovement.MovementMode.Neutral:
                return "Neutre";
            default:
                return mode.ToString();
        }
    }
}