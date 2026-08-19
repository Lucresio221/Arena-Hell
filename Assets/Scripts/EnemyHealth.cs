using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;

    public void TakeDamage(float amount)
    {

        //later add effects
        health -= amount;
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
