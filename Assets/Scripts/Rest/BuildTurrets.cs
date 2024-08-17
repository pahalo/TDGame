using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTurrets : MonoBehaviour
{
    public GameObject turretPrefab; // The prefab to instantiate
    private int nextTurretID = 0; // Variable to keep track of the next turret ID

    // Method to place the turret at the given position
    public void PlaceTurretAtPosition(Vector3 position)
    {
        if (turretPrefab != null)
        {
            // Instantiate the turret at the given position
            GameObject newTurret = Instantiate(turretPrefab, position, Quaternion.identity);

            // Set the turret ID
            SetTurretID(newTurret);
        }
        else
        {
            Debug.LogError("Turret prefab is not assigned.");
        }
    }

    // Method to set the turret ID
    private void SetTurretID(GameObject turret)
    {
        // Get the TurretStats component from the new turret
        TurretStats turretStats = turret.GetComponent<TurretStats>();
        if (turretStats != null)
        {
            turretStats.turretID = nextTurretID;
        }
        else
        {
            Debug.LogError("TurretStats component missing on the instantiated turret.");
        }
        // Count the ID up for the next turret
        nextTurretID++;
    }
}
