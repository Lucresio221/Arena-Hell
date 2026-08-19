using UnityEngine;
using UnityEngine.InputSystem; //Using the new Input system

public class PlayerShooting : MonoBehaviour
{
    public float range = 100f; // Max shooting distance

    void Update()
    {
        // Check if the left button of the mouse was pressed this frame (new input system)
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Create a ray from the center of the screen (0.5, 0.5) through the main camera
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit; // variable that stores info about what the raycast hits

        // Throw the raycast (ray) then if it hits something, out hit saves the info about the object hit
        if (Physics.Raycast(ray, out hit, range))
        {
            // Print the name of the object hit in the console (for now)
            Debug.Log("Impacto en: " + hit.transform.name);

            //Create a EnemyHealth variable so that it can access the TakeDamage function
            EnemyHealth Enemy = hit.transform.GetComponent<EnemyHealth>();
            if (Enemy != null)
            {
                Enemy.TakeDamage(25);
            }
        }
    }
}