using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;           
    public Transform player;                
    public float spawnInterval = 2f;        
    public int maxEnemies = 8;              
    public Transform[] spawnPoints;          

    private float timer;
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        activeEnemies.RemoveAll(enemy => enemy == null);

        if (activeEnemies.Count < maxEnemies)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                TrySpawnAtRandomPoint();
                timer = spawnInterval;
            }
        }
    }

    void TrySpawnAtRandomPoint()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No hay puntos de spawn asignados en el spawner.");
            return;
        }

        Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        SpawnEnemy(randomPoint.position);
    }

    void SpawnEnemy(Vector2 position)
    {
        GameObject newEnemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        Enemy enemyScript = newEnemy.GetComponent<Enemy>();

        if (enemyScript != null)
        {
            enemyScript.player = player;
        }

        activeEnemies.Add(newEnemy);
    }
}