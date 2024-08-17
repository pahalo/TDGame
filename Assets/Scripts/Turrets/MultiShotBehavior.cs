using UnityEngine;

public class MultiShotBehavior : MonoBehaviour, IShootingBehavior
{
    private int currentSpawnIndex = 0;

    public void Shoot(Transform[] bulletSpawnPoints, GameObject bulletPrefab, float turretDamage, Transform target)
    {
        if (bulletPrefab != null && bulletSpawnPoints.Length > 0)
        {
            // Get the spawn point for this shot
            Transform spawnPoint = bulletSpawnPoints[currentSpawnIndex];

            // Instantiate and configure the bullet
            GameObject bulletObject = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            Bullet bulletComponent = bulletObject.GetComponent<Bullet>();
            if (bulletComponent != null)
            {
                bulletComponent.SetDamage(turretDamage);
                bulletComponent.SetTarget(target);
            }

            // Move to the next spawn point
            currentSpawnIndex = (currentSpawnIndex + 1) % bulletSpawnPoints.Length;
        }
        else
        {
            Debug.LogError("Bullet prefab or spawn points not set.");
        }
    }
}
