using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Bullet prefab to spawn
    public Transform bulletSpawnPoint; // Point where bullets are spawned
    [HideInInspector]
    public float shotSpeed; // Shooting speed, set by TurretStats
    [HideInInspector]
    public float turretDamage; // Damage value, set by TurretStats
    private float fireCountdown = 0f; // Countdown to next shot

    private TurretAiming turretAiming; // Reference to the TurretAiming script

    void Start()
    {
        // Getting this Component to know when to shoot
        turretAiming = GetComponent<TurretAiming>();
        if (turretAiming == null)
        {
            Debug.LogError("TurretAiming component missing on the object.");
        }
    }


    void Update()
    {
        if (fireCountdown <= 0f && turretAiming.readyToShoot)
        {
            Shoot();
            fireCountdown = shotSpeed; // Reset countdown based on the shooting speed
        }
        fireCountdown -= Time.deltaTime; // Decrease countdown every second
    }

    void Shoot()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            // Creating a new bullet at the spawnpoint
            GameObject bulletObject = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Bullet bulletComponent = bulletObject.GetComponent<Bullet>();
            if (bulletComponent != null)
            {
                bulletComponent.SetDamage(turretDamage); // Set the current turret damage
                bulletComponent.SetTarget(turretAiming.target); // Set the current target
            }
        }
        else
        {
            Debug.LogError("Bullet prefab or spawn point not set.");
        }
    }
}
