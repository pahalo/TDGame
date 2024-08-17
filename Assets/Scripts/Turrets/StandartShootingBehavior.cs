using UnityEngine;

public class StandardShootingBehavior : MonoBehaviour, IShootingBehavior
{
    public void Shoot(Transform[] bulletSpawnPoints, GameObject bulletPrefab, float turretDamage, Transform target)
    {
        // Ensure there is a valid bullet prefab and at least one spawn point
        if (bulletPrefab != null && bulletSpawnPoints.Length > 0)
        {
            foreach (Transform spawnPoint in bulletSpawnPoints)
            {
                // Instantiate the bullet at the spawn point's position and rotation
                GameObject bulletObject = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
                
                // Get the Bullet component from the instantiated bullet object
                Bullet bulletComponent = bulletObject.GetComponent<Bullet>();
                
                // If the Bullet component exists, set its damage and target
                bulletComponent.SetDamage(turretDamage);
                bulletComponent.SetTarget(target);
                
            }
        }
        else
        {
            // Log an error if the bullet prefab or spawn points are not set correctly
            Debug.LogError("Bullet prefab or spawn points not set.");
        }
    }
}
