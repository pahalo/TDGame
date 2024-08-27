using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTurrets : MonoBehaviour
{
    public GameObject[] turretPrefabs; // The List of Prefabs to choose, which will be built
    private int selectedTurretIndex = 0;  // Index to keep track of the selected turret type
    private int nextTurretID = 0; // Variable to keep track of the next turret ID

    private SupportTurret supportTurret;
    private GameManager gameManager;

    private void Start() {
        // Find the GameManager in the scene
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }

    // Method to select turret type based on key pressed
    public void SelectTurretType(int turretIndex)
    {
        if (turretIndex >= 0 && turretIndex < turretPrefabs.Length)
        {
            selectedTurretIndex = turretIndex;
        }
        else
        {
            Debug.LogWarning("Invalid turret index selected.");
        }
    }

    // Method to place the turret at the given position and if the turret is build because a scene is loaded, then it shall be free
    public void PlaceTurretAtPosition(Vector3 position, bool freePlacement = false, int? turretIndex = null)
    {
        int indexToUse = turretIndex.HasValue ? turretIndex.Value : selectedTurretIndex;

        if (turretPrefabs[indexToUse] != null)
        {   
            // Get the TurretStats script of the turret to build in order to get the turret cost
            TurretStats turretStats = turretPrefabs[indexToUse].GetComponent<TurretStats>();

            // If freePlacement is true, skip the money check and cost deduction
            if(freePlacement || gameManager.SpendMoney(turretStats.GetTurretCost())){
                // Instantiate the chosen turret at the given position
                GameObject newTurret = Instantiate(turretPrefabs[indexToUse], position, Quaternion.identity);

                // Set the turret ID
                SetTurretID(newTurret);

                // Set the turret index if not provided
                newTurret.GetComponent<TurretStats>().SetTurretIndex(indexToUse);

                // Call all SupportTurret scripts to ensure that the new instantiated tower has the correct buffs
                SupportTurret[] allSupportTurrets = FindObjectsOfType<SupportTurret>();

                // Call UpdateSupportTurretEffect on all SupportTurret instances
                foreach (SupportTurret supportTurret in allSupportTurrets)
                {
                    supportTurret.UpdateSupportTurretEffect();
                }
            } 
            else 
            {
                Debug.Log("Not enough money to build this turret");
            }
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
            turretStats.SetTurretID(nextTurretID);
        }
        else
        {
            Debug.LogError("TurretStats component missing on the instantiated turret.");
        }
        // Increment the ID for the next turret
        nextTurretID++;
    }
}
