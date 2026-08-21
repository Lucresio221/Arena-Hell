using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    void Start()
    {
        currentHealth = maxHealth; //Start with full health
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount; // Reduce health by the damage amount received in PlayerShooting.cs
        Debug.Log("Vida restante del Jugador: " + currentHealth); // Print current health for now

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("El Jugador ha muerto.");
        // Game over logic will be implemented here
    }
}