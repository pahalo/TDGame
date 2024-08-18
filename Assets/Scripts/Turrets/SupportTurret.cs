using System.Collections.Generic;
using UnityEngine;

public class SupportTurret : MonoBehaviour
{
    // Reference to the TurretStats component attached to this turret
    private TurretStats turretStats;

    // List to temporarily store turrets within range for processing
    private List<TurretStats> turretsInRange = new List<TurretStats>();

    // Dictionary to keep track of which turrets have already been buffed
    private Dictionary<TurretStats, bool> alreadySupported = new Dictionary<TurretStats, bool>();

    void Start()
    {
        // Get the TurretStats component attached to this game object
        turretStats = GetComponent<TurretStats>();

        // If no TurretStats component is found, log an error
        if (turretStats == null)
        {
            Debug.LogError("TurretStats component not found!");
        }
        UpdateSupportTurretEffect();
    }

    // This method applies the support turret's effects to nearby turrets
    public void UpdateSupportTurretEffect()
    {
        if (turretStats != null)
        {
            // Clear the list of turrets in range for a fresh calculation
            turretsInRange.Clear();

            // Find all TurretStats components in the scene
            TurretStats[] allTurrets = FindObjectsOfType<TurretStats>();

            // First pass: Find all turrets within range
            foreach (TurretStats turret in allTurrets)
            {
                if (turret == turretStats)
                {
                    continue;
                }

                float distance = Vector3.Distance(transform.position, turret.transform.position);

                if (distance <= turretStats.GetTurretRange())
                {
                    turretsInRange.Add(turret);
                }
            }

            // Second pass: Apply the effects to all turrets in range
            foreach (TurretStats turret in turretsInRange)
            {
                // Check if this turret has already been supported
                if (!alreadySupported.ContainsKey(turret) || !alreadySupported[turret])
                {
                    // Apply the support effects
                    foreach (var attribute in turretStats.turretAttributes)
                    {
                        switch (attribute.attributeType)
                        {
                            case TurretStats.AttributeType.Range:
                                turret.SetTurretRange(turret.GetTurretRange() * attribute.value);
                                break;

                            case TurretStats.AttributeType.Attack:
                                turret.SetTurretDamage(turret.GetTurretDamage() * attribute.value);
                                break;

                            case TurretStats.AttributeType.Speed:
                                turret.SetTurretShotSpeed(turret.GetTurretShotSpeed() / attribute.value);
                                break;

                            default:
                                Debug.LogWarning("Unknown attribute type");
                                break;
                        }
                    }

                    // Mark this turret as supported
                    alreadySupported[turret] = true;
                }
            }
        }
    }
}
