using UnityEngine;

public class UpgradeTurrets : MonoBehaviour
{
    public void UpgradeTurret(TurretStats turretStats)
    {
        // Check if there is a next upgrade prefab assigned
        if (turretStats.GetNextUpgradePrefab() == null)
        {
            return;
        }

        // Get the GameManager and compare the Player Money and the Turret Cost
        if (GameManager.GetInstance().SpendMoney(turretStats.GetTurretCost()))
        {
            // Get the position and rotation of the current turret
            Vector3 position = turretStats.transform.position;
            Quaternion rotation = turretStats.transform.rotation;

            // Store the current turret ID to transfer it to the new turret
            int oldTurretID = turretStats.GetTurretID();

            // Destroy the current turret before replacing it
            Destroy(turretStats.gameObject);

            // Instantiate the new turret prefab at the same position and rotation
            GameObject newTurret = Instantiate(turretStats.GetNextUpgradePrefab(), position, rotation);

            // Call all Support Turrets scripts to make sure that the new instantiated tower has the correct Buffs
            SupportTurret[] allSupportTurrets = FindObjectsOfType<SupportTurret>();

            // Call UpdateSupportTurretEffect on all SupportTurret instances
            foreach (SupportTurret supportTurret in allSupportTurrets)
            {
                supportTurret.UpdateSupportTurretEffect();
            }

            // Get the TurretStats component from the newly instantiated turret
            TurretStats newTurretStats = newTurret.GetComponent<TurretStats>();

            // Ensure that the new turret has a TurretStats component before proceeding
            if (newTurretStats != null)
            {
                // Set the level of the new turret to be one higher than the previous turret
                newTurretStats.SetTurretLevel(turretStats.GetTurretLevel() + 1);

                // Transfer the turret ID from the old turret to the new turret
                newTurretStats.SetTurretID(oldTurretID);
            }
            else
            {
                // Log an error if the TurretStats component is not found on the new turret
                Debug.LogError("TurretStats component not found on the new turret.");
            }
        }
        else
        {
            Debug.Log("Not enough money to upgrade this turret.");
        }
    }
}
