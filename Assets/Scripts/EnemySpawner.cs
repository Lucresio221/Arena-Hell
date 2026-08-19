using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // get the prefab
    public float spawnDelay = 2f;  // delay for spawn
    public float spawnInterval = 4f; // in-between time which enemies spawns
    public Transform[] spawnPoints; // get the transforms
    void Start()
    {
        // instantiates enemies using the variables up here
        InvokeRepeating("SpawnEnemy", spawnDelay, spawnInterval);
    }

    void SpawnEnemy()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        // finds teh amount of enemies
        if (enemyPrefab != null && enemyCount < 3)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length); // selects between different spawnpoints
            Transform selectedPoint = spawnPoints[randomIndex];

            // once selected, instantiates the prefab
            Instantiate(enemyPrefab, selectedPoint.position, selectedPoint.rotation);
        }
    }
}