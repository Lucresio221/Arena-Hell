using UnityEngine;
using UnityEngine.InputSystem; // Inclusión obligatoria para el nuevo Input System

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // Velocidad de movimiento del jugador

    private CharacterController controller;

    void Start()
    {
        // Obtener la referencia al CharacterController del jugador
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Verificar que el teclado esté conectado
        if (Keyboard.current == null) return;

        float x = 0f;
        float z = 0f;

        // Leer entradas de movimiento usando el nuevo Input System
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
    }
}