using UnityEngine;

public class TurretAiming : MonoBehaviour
{
    public Transform partToRotate; // The part of the turret that should rotate to face the enemy
    [HideInInspector]
    public float turnSpeed; // Speed at which the turret turns, set by TurretStats
    public float rotationAngle; // Angle which will be added on the y rotation of the turret
    [HideInInspector]
    public bool readyToShoot = false; // Indicates if the turret has a valid target and is ready to shoot
    [HideInInspector]
    public Transform target; // Current target enemy

    void Update()
    {
        UpdateTarget();
        if (target != null)
        {
            LockOnTarget();
        } 
        else 
        {
            // If there is no target the turret will not shoot
            readyToShoot = false;
        }
    }

    void UpdateTarget()
    {
        // Find all enemies in the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void LockOnTarget()
    {
        readyToShoot = true;
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(partToRotate.rotation.eulerAngles.x, rotationAngle + rotation.y, partToRotate.rotation.eulerAngles.z);
    }
}
