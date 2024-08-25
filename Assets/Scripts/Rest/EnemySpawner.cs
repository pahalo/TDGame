using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public List<GameObject> enemyPrefabs; // List of enemy prefabs for this wave
    public List<int> enemyCounts;         // Number of enemies per type
    public List<float> spawnIntervals;    // Spawn intervals between types
}

public class EnemySpawner : MonoBehaviour
{
    private Transform spawnPoint;

    public List<Wave> waves;              // List of waves for the level
    private int currentWaveIndex = 0;     // Current wave index

    void Start()
    {
        Road road = FindObjectOfType<Road>();
        if (road != null)
        {
            List<Transform> waypoints = road.GetRoadPoints();
            if (waypoints != null && waypoints.Count > 0)
            {
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
        if (PauseMenu.gameIsPaused) { return; }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Spawning wave: " + currentWaveIndex);
            if (currentWaveIndex < waves.Count)
            {
                StartCoroutine(SpawnWave(waves[currentWaveIndex]));
                currentWaveIndex++;
            }
            else
            {
                Debug.Log("Won");
            }
        }
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        for (int i = 0; i < wave.enemyPrefabs.Count; i++)
        {
            for (int j = 0; j < wave.enemyCounts[i]; j++)
            {
                SpawnEnemy(wave.enemyPrefabs[i]);
                yield return new WaitForSeconds(wave.spawnIntervals[i]);
            }
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        if (spawnPoint != null)
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogError("Spawn point not set.");
        }
    }
}
