using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{   
    public float enemyMovementSpeed = 5f;

    // List of all points the Enemy has to get through
    // Has to be instantiated in the Start Method
    private List<Transform> roadPoints;
    // The Point the Enemy is trying to get to
    private int currentPointIndex = 0;

    void Start()
    {
        // Find the Road object in the scene and get the waypoints
        Road road = GameObject.FindObjectOfType<Road>();
        if (road != null)
        {
            // Assign roadPoints directly
            roadPoints = road.GetRoadPoints();

            // Check if roadPoints is still null or empty
            if (roadPoints == null || roadPoints.Count == 0)
            {
                Debug.LogError("No waypoints found in the Road object.");
            }
        } else {
            // Only happens in case no road object was found
            Debug.Log("No road object found");
        }
    }

    void Update()
    {
        // If no waypoints are available, do nothing
        if (roadPoints == null || roadPoints.Count == 0) Debug.Log("la");

        // Move the enemy towards the current waypoint
        MoveToPoint(roadPoints[currentPointIndex]);
    }

    private void MoveToPoint(Transform point)
    {
        // Calculate the direction to the current waypoint
        Vector3 direction = (point.position - transform.position).normalized;

        // Move towards the waypoint
        transform.position += direction * enemyMovementSpeed * Time.deltaTime;

        // Check if the enemy has reached the waypoint
        if (Vector3.Distance(transform.position, point.position) < 0.1f)
        {
            currentPointIndex++;

            // If the last waypoint is reached, handle what happens next (e.g., destroy the enemy)
            if (currentPointIndex >= roadPoints.Count)
            {
                // Example: Enemy reaches the end and is destroyed
                Debug.Log("Last waypoint got reached");
                Destroy(gameObject);
            }
        }
    }
}
