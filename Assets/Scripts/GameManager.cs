using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    
    [Header("UI components")]
    public TextMeshProUGUI healthTxt;
    public TextMeshProUGUI scoreTxt;
    private PlayerHealth playerCurrentHealth;
    public int score = 0;
    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
        playerCurrentHealth = FindFirstObjectByType<PlayerHealth>();
    }

    void Update()
    {
        if (healthTxt != null && playerCurrentHealth != null )
        {
            healthTxt.text = "Health: " + playerCurrentHealth.currentHealth;
        }
    }

    public void AddScore()
    {
        score++;
        scoreTxt.text = "Score: " + score;
    }

}