using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    // List of waypoint transforms (assigned in the Unity editor)
    [SerializeField]
    private List<Transform> roadPoints;

    // Public method to retrieve the waypoints
    public List<Transform> GetRoadPoints()
    {
        return roadPoints;
    }
}
