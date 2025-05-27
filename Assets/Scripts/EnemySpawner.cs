using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;           // Prefab del enemigo
    public Transform player;                 // Referencia al jugador
    public float spawnInterval = 2f;         // Tiempo entre spawns
    public int maxEnemies = 8;               // Máximo de enemigos activos

    public Transform[] spawnPoints;          // Arreglo con los 5 puntos de spawn

    private float timer;
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        // Limpiar enemigos destruidos
        activeEnemies.RemoveAll(enemy => enemy == null);

        // Solo generar si hay menos del máximo permitido
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

        // Selecciona uno de los 5 puntos aleatoriamente
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