using UnityEngine;

public class UpgradeTurrets : MonoBehaviour
{
    private GameManager gameManager;
    private BuildTurrets buildTurrets;

    private void Start()
    {
        // Find the GameManager in the scene
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }


        buildTurrets = FindObjectOfType<BuildTurrets>();
        if (buildTurrets == null)
        {
            Debug.LogError("BuildTurrets not found in the scene.");
        }
    }

    public void UpgradeTurret(TurretStats turretStats, int selectedPathIndex)
    {
        // Get the next upgrade prefab based on the selected path
        GameObject nextPrefab = turretStats.GetNextUpgradePrefab(selectedPathIndex);
        if (nextPrefab == null)
        {
            return;
        }

        // Compare the player's money and the turret upgrade cost
        if (gameManager.SpendMoney(turretStats.GetTurretCost()))
        {
            // Get the position and rotation of the current turret
            Vector3 position = turretStats.transform.position;
            Quaternion rotation = turretStats.transform.rotation;

            // Store the current turret ID to transfer it to the new turret
            int oldTurretID = turretStats.GetTurretID();

            // Destroy the current turret before replacing it
            Destroy(turretStats.gameObject);

            // Instantiate the new turret prefab at the same position and rotation
            GameObject newTurret = Instantiate(nextPrefab, position, rotation);

            // Call all SupportTurret scripts to ensure the new turret has the correct buffs
            SupportTurret[] allSupportTurrets = FindObjectsOfType<SupportTurret>();

            // Call UpdateSupportTurretEffect on all SupportTurret instances
            foreach (SupportTurret supportTurret in allSupportTurrets)
            {
                supportTurret.UpdateSupportTurretEffect();
            }

            // Get the TurretStats component from the newly instantiated turret
            TurretStats newTurretStats = newTurret.GetComponent<TurretStats>();

            // Ensure the new turret has a TurretStats component before proceeding
            if (newTurretStats != null)
            {
                // Transfer the turret ID from the old turret to the new turret
                newTurretStats.SetTurretID(oldTurretID);

                // Set the new turret index based on the upgraded prefab
                // Assuming that the prefab's position in turretPrefabs list determines its index
                for (int i = 0; i < buildTurrets.turretPrefabs.Length; i++)
                {
                    if (buildTurrets.turretPrefabs[i] == nextPrefab)
                    {
                        newTurretStats.SetTurretIndex(i);
                        break;
                    }
                }
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
