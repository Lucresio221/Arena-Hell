using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public float range = 100f; // Distancia máxima del disparo

    void Update()
    {
        // Detectar el clic izquierdo en el frame actual
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Crear un rayo desde el centro de la pantalla (0.5, 0.5) a través de la cámara principal
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // Lanzar el Raycast físico
        if (Physics.Raycast(ray, out hit, range))
        {
            // Imprimir en la consola el nombre del objeto impactado
            Debug.Log("Impacto en: " + hit.transform.name);


            EnemyHealth Enemy = hit.transform.GetComponent<EnemyHealth>();
            if (Enemy != null)
            {
                Enemy.TakeDamage(25);
            }
        }
    }
}