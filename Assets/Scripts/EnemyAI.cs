using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform playerTransform;

    // ---
    private PlayerHealth playerHealth; // PlayerHealth type of variable
    
    [Header("Attack settings")]
    public float attackRange = 1.5f;     // Attack distance
    public float attackInterval = 1.5f;  // Attack every 1.5m distance
    public float damageAmount = 25f;     // Attack damage
    
    private float attackTimer = 0f;

    void Start()
    {
        //get the agent, we have to tell it that its a navmesh
        agent = GetComponent<NavMeshAgent>();

        //we find the player by name
        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;

            playerHealth = playerObj.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // keeps the destination updated
            agent.SetDestination(playerTransform.position);
        }


        // use Time.deltaTime as a counter
        attackTimer += Time.deltaTime;

        if (playerTransform != null)
        {
            // calculates direction and position squared (for some reason, idk)
            Vector3 direction = playerTransform.position - transform.position;
            float squareDistance = direction.sqrMagnitude;

            // range (again squared, idk)
            float squareAttackRange = attackRange * attackRange;

            // attack if range and timer are right
            if (squareDistance <= squareAttackRange)
            {
                if (attackTimer >= attackInterval)
                {
                    // use the PlayerHealth variable to call the func TakeDamage
                    playerHealth.TakeDamage(damageAmount);
                    
                    // Restart timer
                    attackTimer = 0f;
                }
            }
        }
    }
}