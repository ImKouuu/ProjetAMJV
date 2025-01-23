using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 1000f;
    [SerializeField] private float moveSpeed = 10f;

    private bool isRightClickHeld = false;

    void Start()
    {
        // Initialement, le curseur est disponible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        HandleMovement();

        if (Input.GetMouseButtonDown(1))
        {
            isRightClickHeld = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isRightClickHeld = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (isRightClickHeld)
        {
            HandleMouseLook();
        }
    }

    private void HandleMovement()
    {
        // Gestion des mouvements avec ZQSD
        if (Input.GetKey(KeyCode.Z))
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.Q))
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    private void HandleMouseLook()
    {
        // Récupération des mouvements de souris
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotation verticale (pitch)
        transform.Rotate(Vector3.left * mouseY);

        // Rotation horizontale (yaw)
        transform.Rotate(Vector3.up * mouseX, Space.World);
    }
}