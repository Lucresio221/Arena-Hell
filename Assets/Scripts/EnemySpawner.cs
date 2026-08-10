using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Asigna el Prefab aquí desde el Inspector
    public float spawnDelay = 2f;  // Tiempo de espera antes del primer spawn
    public float spawnInterval = 4f; // Tiempo entre cada spawn
    public Transform[] spawnPoints; // Lista de puntos de aparición
    void Start()
    {
        // Inicia la repetición automática del método SpawnEnemy
        InvokeRepeating("SpawnEnemy", spawnDelay, spawnInterval);
    }

    void SpawnEnemy()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        // Verifica que hayamos asignado el prefab para evitar errores
        if (enemyPrefab != null && enemyCount < 3)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform selectedPoint = spawnPoints[randomIndex];

            // Instancia el enemigo en la posición y rotación del punto elegido
            Instantiate(enemyPrefab, selectedPoint.position, selectedPoint.rotation);
        }
    }
}