using UnityEngine;

public class TurretStats : MonoBehaviour
{
    [SerializeField]
    private float turretTurnSpeed = 10f; // Default turn speed
    [SerializeField]
    private float turretDamage = 10f; // Schadenswert des Turms
    [SerializeField]
    private float turretShotSpeed = 2f; // Schussgeschwindigkeit des Turms

    private TurretAiming turretAiming;
    private TurretShooting turretShooting;

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
    }
}
