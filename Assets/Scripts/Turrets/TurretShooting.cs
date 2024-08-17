using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Bullet prefab to spawn
    public Transform[] bulletSpawnPoints; // Array of points where bullets are spawned
    [HideInInspector]
    public float shotSpeed; // Shooting speed, set by TurretStats
    [HideInInspector]
    public float turretDamage; // Damage value, set by TurretStats
    private float fireCountdown = 0f; // Countdown to next shot

    private TurretAiming turretAiming; // Reference to the TurretAiming script
    private IShootingBehavior shootingBehavior; // Reference to the shooting behavior

    void Start()
    {
        // Getting this Component to know when to shoot
        turretAiming = GetComponent<TurretAiming>();
        if (turretAiming == null)
        {
            Debug.LogError("TurretAiming component missing on the object.");
        }
        // Getting the shooting Behavior Component to then call different shooting behaviors depening on the turret (single shot, multishot, etc.)
        shootingBehavior = GetComponent<IShootingBehavior>();
        if (shootingBehavior == null)
        {
            Debug.LogError("ShootingBehavior component missing on the object.");
        }
    }


    void Update()
    {
        if (fireCountdown <= 0f && turretAiming.readyToShoot)
        {
            // Calling the shooting Behavior Interface which allows for different shooting behaviors depending on the turrets type
            shootingBehavior.Shoot(bulletSpawnPoints, bulletPrefab, turretDamage, turretAiming.target);
            fireCountdown = shotSpeed; // Reset countdown based on the shooting speed
        }
        fireCountdown -= Time.deltaTime; // Decrease countdown every second
    }
}
