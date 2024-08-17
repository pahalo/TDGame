using UnityEngine;

// With this Interface different turret shooting mechanisms can be created, by giving these values and then builing a custom behaivior shooting script for turrets
public interface IShootingBehavior
{
    void Shoot(Transform[] bulletSpawnPoints, GameObject bulletPrefab, float turretDamage, Transform target);
}
