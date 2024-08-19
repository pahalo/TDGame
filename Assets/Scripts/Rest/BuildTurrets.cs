using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTurrets : MonoBehaviour
{
    public GameObject[] turretPrefabs; // The List of Prefabs to choose, which will be build
    private int selectedTurretIndex = 0;  // Index to keep track of the selected turret type
    private int nextTurretID = 0; // Variable to keep track of the next turret ID
    
    private SupportTurret supportTurret;

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

    // Method to place the turret at the given position
    public void PlaceTurretAtPosition(Vector3 position)
    {
        if (turretPrefabs[selectedTurretIndex] != null)
        {   
            // Get the turretStats script of the turret to Build in order to get the turret cost
            TurretStats turretStats = turretPrefabs[selectedTurretIndex].GetComponent<TurretStats>();

            // Get the GameManager and compare the Player Money and the Turret Cost
            if(GameManager.GetInstance().SpendMoney(turretStats.GetTurretCost())){
                // Instantiate the choosen turret at the given position
                GameObject newTurret = Instantiate(turretPrefabs[selectedTurretIndex], position, Quaternion.identity);

                // Set the turret ID
                SetTurretID(newTurret);

                // Call all Support Turrets scripts to make sure that the new instantiated tower has the correct Buffs
                SupportTurret[] allSupportTurrets = FindObjectsOfType<SupportTurret>();

                // Call UpdateSupportTurretEffect on all SupportTurret instances
                foreach (SupportTurret supportTurret in allSupportTurrets)
                {
                    supportTurret.UpdateSupportTurretEffect();
                }
            } else {
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
        // Count the ID up for the next turret
        nextTurretID++;
    }
}
