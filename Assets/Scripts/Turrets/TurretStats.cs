using UnityEngine;

public class TurretStats : MonoBehaviour
{
    [SerializeField]
    private float turretTurnSpeed = 10f; // Default turn speed
    [SerializeField]
    private float turretDamage = 10f; // Turret Damage
    [SerializeField]
    private float turretShotSpeed = 2f; // Turrets reload speed in seconds
    public int turretID; // Turret Id which will be used to know which turret was selected and which is given through the BuildTurret script
    [SerializeField]
    private float turretRange = 3f; // Range in which the will rotate and shoot at the Enemy
    [SerializeField]
    private float turretDistanceToOtherTurrets = 1f; // The nearest point arround a turret where an other turret can be build

    private TurretAiming turretAiming;
    private TurretShooting turretShooting;
    private RangeIndicator rangeIndicator;

    private void Start()
    {
        // Get the Aiming Component to set stats which are important for turning
        turretAiming = GetComponent<TurretAiming>();
        if (turretAiming == null)
        {
            Debug.LogError("TurretAiming component missing on the object.");
            return;
        }
        // Set the turn speed in TurretAiming
        turretAiming.turnSpeed = turretTurnSpeed;
        SetTurretRange(turretRange);

        // Get the Shooting Component to set stats which are important for shooting
        turretShooting = GetComponent<TurretShooting>();
        if (turretShooting == null)
        {
            Debug.LogError("TurretShooting component missing on the object.");
            return;
        }
        // Set the shot speed and damage in TurretShooting
        // The damage will later be given to the bullet thorugh the Turret Shooting script because it knows which bullet it fired
        turretShooting.shotSpeed = turretShotSpeed;
        turretShooting.turretDamage = turretDamage;

        // Get the RangeIndicator Component from the child object
        rangeIndicator = GetComponentInChildren<RangeIndicator>();
        if (rangeIndicator != null)
        {
            // Set the range indicator radius based on turretDistanceToOtherTurrets
            rangeIndicator.SetRadius(turretDistanceToOtherTurrets);
        }
        else
        {
            Debug.LogWarning("RangeIndicator component not found in child objects.");
        }
    }
    
    // Method to set the turret's range in the TurretAiming script
    public void SetTurretRange(float range)
    {
        turretRange = range;

        if (turretAiming != null)
        {
            turretAiming.SetRange(turretRange);
        }
    }
    // Draw the turret range using Gizmos
    private void OnDrawGizmosSelected()
    {
        // Set the Gizmo color to green
        Gizmos.color = Color.green;
        // Draw a wire sphere at the turret's position with a radius of turretRange
        Gizmos.DrawWireSphere(transform.position, turretRange);
    }
}
