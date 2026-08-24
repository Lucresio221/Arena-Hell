using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    [SerializeField] private GameObject gameOverPanel;

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
        gameOverPanel.SetActive(true);

        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true ;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(0);
    }
}