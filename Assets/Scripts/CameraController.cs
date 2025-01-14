using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 1000f;
    [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private float xRotation = 0f; // Rotation verticale (pitch)
    [SerializeField] private float yRotation = 0f; // Rotation horizontale (yaw)

    void Start()
    {
        // Verrouiller le curseur
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        bool rightClick = Input.GetMouseButtonDown(1);
        HandleMovement();
        if (rightClick)
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
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Empêche de regarder trop haut/bas

        // Rotation horizontale (yaw)
        yRotation += mouseX;

        // Applique les rotations
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
