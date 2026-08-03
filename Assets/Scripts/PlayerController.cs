using UnityEngine;
using UnityEngine.InputSystem; // Inclusión obligatoria para el nuevo Input System

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 5.0f; // Velocidad de movimiento del jugador

    [Header("Cámara y Visión")]
    public Transform cameraTransform; // Arrastra la Main Camera aquí en el Inspector
    public float mouseSensitivity = 0.1f; // Sensibilidad para Mouse.current

    private CharacterController controller;
    private float xRotation = 0f;

    void Start()
    {
        // Obtener la referencia al CharacterController del jugador
        controller = GetComponent<CharacterController>();

        // Bloquear el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // --- 1. ROTACIÓN CON EL MOUSE (NUEVO INPUT SYSTEM) ---
        if (Mouse.current != null)
        {
            // Leer el movimiento del mouse
            Vector2 mouseDelta = Mouse.current.delta.ReadValue() * mouseSensitivity;

            // Inclinación vertical (Cámara)
            xRotation -= mouseDelta.y;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            if (cameraTransform != null)
            {
                cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            }

            // Giro horizontal (Jugador)
            transform.Rotate(Vector3.up * mouseDelta.x);
        }

        // --- 2. MOVIMIENTO WASD (NUEVO INPUT SYSTEM) ---
        if (Keyboard.current != null)
        {
            float x = 0f;
            float z = 0f;

            // Leer entradas de movimiento
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) x -= 1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) x += 1f;
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) z += 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) z -= 1f;

            // Normalizar para no moverse más rápido en diagonal
            Vector3 moveInput = new Vector3(x, 0, z).normalized;

            // Calcular la dirección del movimiento orientada a la mirada del jugador
            Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.z;

            // Mover al jugador a través del CharacterController
            controller.Move(move * speed * Time.deltaTime);

            // Desbloquear el cursor con Escape
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}