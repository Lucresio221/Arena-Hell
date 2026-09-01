using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;
    public float maxHealth = 100f;
    public float currentHealth;
    public float smoothSpeed = 5f;
    private float targetHealth;
    [SerializeField] private GameObject gameOverPanel;

    // Added a delay setting so the health bar animation can complete before showing GameOver UI
    [SerializeField] private float delayBeforeGameOver = 1.0f;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth; // Start with full health
        targetHealth = maxHealth;  // Initialize targetHealth with full health to prevent initial lerp to zero
        healthSlider.maxValue = maxHealth; // Set the maximum value of the health slider to the maximum health
        healthSlider.value = currentHealth; // Set the current value of the health slider to the current health
    }

    void Update()
    {
        // Smoothly update the health slider value using Time.unscaledDeltaTime 
        // to ensure the animation completes even when Time.timeScale is set to 0
        healthSlider.value = Mathf.Lerp(healthSlider.value, currentHealth, smoothSpeed * Time.unscaledDeltaTime);
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return; // Prevent receiving damage multiple times after dying

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth); // Clamp current health value

        targetHealth = currentHealth; // Keep targetHealth synchronized with currentHealth

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        // Disable player actions immediately upon death
        GetComponent<PlayerController>().enabled = false;
        GetComponent<PlayerShooting>().enabled = false;

        // Start the death sequence coroutine to allow the health bar animation to finish
        StartCoroutine(DieSequence());
    }

    
    //Handles the game over delay in real-time, pausing the game after the UI bar animates.
    private IEnumerator DieSequence()
    {
        // Wait in real-time for the slider interpolation to reach zero smoothly
        yield return new WaitForSecondsRealtime(delayBeforeGameOver);

        gameOverPanel.SetActive(true);

        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(0);
    }
}