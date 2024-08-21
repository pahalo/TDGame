using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
     // The prefab of the enemy to spawn
    public GameObject enemyPrefab;
    // The point where enemies should be spawned, set to the first element of the roadPoints list
    private Transform spawnPoint;

    void Start()
    {
        // Find the Road component in the scene and get the first waypoint as the spawn point
        Road road = FindObjectOfType<Road>();
        if (road != null)
        {
            List<Transform> waypoints = road.GetRoadPoints();
            if (waypoints != null && waypoints.Count > 0)
            {
                // Set spawnPoint to the first waypoint
                spawnPoint = waypoints[0];
            }
            else
            {
                Debug.LogError("No waypoints found in Road component.");
            }
        }
        else
        {
            Debug.LogError("No Road component found in the scene.");
        }
    }

    void Update()
    {
        // Dont spawn enmies if the game is paused
        if(PauseMenu.gameIsPaused == true) { return; }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnEnemy();
        }
    }

    // Creates an instance of the enemy at the spawn point
    private void SpawnEnemy()
    {
        if (enemyPrefab != null && spawnPoint != null)
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogError("Enemy prefab or spawn point not set.");
        }
    }
}
