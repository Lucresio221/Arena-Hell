using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform playerTransform;

    // ---
    private PlayerHealth playerHealth; // Referencia al script PlayerHealth del jugador
    
    [Header("Attack settings")]
    public float attackRange = 1.5f;     // Distancia de ataque en metros
    public float attackInterval = 1.5f;  // Ataca cada 1.5 segundos
    public float damageAmount = 25f;     // Daño por ataque
    
    private float attackTimer = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

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
            // Mantiene actualizado el destino
            agent.SetDestination(playerTransform.position);
        }


        // 1. Acumular el tiempo transcurrido usando Time.deltaTime
        attackTimer += Time.deltaTime;

        if (playerTransform != null)
        {
            // 2. Calcular el vector dirección y la distancia al cuadrado
            Vector3 direction = playerTransform.position - transform.position;
            float squareDistance = direction.sqrMagnitude;

            // 3. Elevar el rango deseado al cuadrado (1.5 * 1.5 = 2.25)
            float squareAttackRange = attackRange * attackRange;

            // 4. Comparar y atacar si está en rango y el temporizador está listo
            if (squareDistance <= squareAttackRange)
            {
                if (attackTimer >= attackInterval)
                {
                    // Aplicar daño
                    playerHealth.TakeDamage(damageAmount);
                    
                    // Reiniciar el temporizador
                    attackTimer = 0f;
                }
            }
        }
    }
}