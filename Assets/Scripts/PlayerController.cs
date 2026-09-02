using UnityEngine;
using UnityEngine.InputSystem; // Needed for new input system

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")] //Just a header for organization
    public float speed = 5.0f; // Movement speed
    public float inertia = 10.0f; // Interpolation speed for inertia (higher = faster response, lower = smoother/heavier)

    [Header("Cámara y Visión")]
    public Transform cameraTransform; // Drag the camera here in the inspector
    public float mouseSensitivity = 0.1f; // Sensitivity for mouse

    private CharacterController controller;
    private float xRotation = 0f; // Stores the vertical rotation of the camera
    private Vector3 currentMove; // Stores the current movement velocity for smooth inertia interpolation

    void Start()
    {
        // Get the CharacterController component attached to the player
        controller = GetComponent<CharacterController>();

        // Lock the cursorand make it invisible at the start
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // --- Mouse rotation (new input system) ---
        if (Mouse.current != null)
        {
            // Reads mouse movement and multiplies it by the sens, stored in a Vector2 variable because the mouse movement is in 2D (X and Y)
            Vector2 mouseDelta = Mouse.current.delta.ReadValue() * mouseSensitivity;

            // Vertical rotation (Camera)
            xRotation -= mouseDelta.y; // xRotation is actually the vertical rotation of the camera, so we subtract the mouse Y movement to invert it
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp the vertical rotation to avoid flipping the camera upside down
            if (cameraTransform != null)
            {
                cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Apply the vertical rotation to the camera
            }

            // Horizontal rotation (Player)
            transform.Rotate(Vector3.up * mouseDelta.x); //Vector 3.up is a shorcut for (0,1,0) which is the Y axis, multiplied by mouseDelta.x
        }

        // --- Movement (New Input System) ---
        if (Keyboard.current != null)
        {
            float x = 0f;
            float z = 0f;

            // Read keyboard input for movement (WASD or arrow keys)
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) x -= 1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) x += 1f;
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) z += 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) z -= 1f;

            // Normalize the movement vector to avoid faster diagonal movement
            Vector3 moveInput = new Vector3(x, 0, z).normalized;

            // Calculate the movement direction based on the player's orientation
            Vector3 targetMove = (transform.right * moveInput.x + transform.forward * moveInput.z) * speed; // Transform.right and .forward are just directions, so the player moves according to it

            // Smoothly interpolate current velocity toward target velocity to add inertia
            currentMove = Vector3.Lerp(currentMove, targetMove, Time.deltaTime * inertia);

            // Move the player using the CharacterController component
            controller.Move(currentMove * Time.deltaTime);

            // Unlock the cursor and make it visible when the Escape key is pressed
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}