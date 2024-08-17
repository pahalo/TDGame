using UnityEngine;

public class TurretAiming : MonoBehaviour
{
    public Transform partToRotate; // Der Teil des Turms, der sich drehen soll
    [HideInInspector]
    public float turnSpeed = 5f; // Geschwindigkeit, mit der sich der Turm dreht
    public float rotationAngle; // Winkel, der zur Y-Drehung des Turms hinzugefügt wird
    [HideInInspector]
    public bool readyToShoot = false; // Gibt an, ob der Turm ein gültiges Ziel hat und schussbereit ist
    private float turretRange; // Reichweite, innerhalb derer der Turm Feinde anvisiert
    [HideInInspector]
    public Transform target; // Aktuelles Ziel

    void Update()
    {
        UpdateTarget();
        if (target != null && Vector3.Distance(transform.position, target.position) <= turretRange)
        {
            LockOnTarget();
        }
        else
        {
            readyToShoot = false;
        }
    }

    private void UpdateTarget()
    {
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

        if (nearestEnemy != null && shortestDistance <= turretRange)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    private void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        dir.y = 0; // Ignore the Y component to focus on horizontal rotation only
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 targetRotation = lookRotation.eulerAngles;
        
        // Calculate the new rotation only on the Y-axis
        float newYRotation = Mathf.LerpAngle(partToRotate.eulerAngles.y, rotationAngle + targetRotation.y, Time.deltaTime * turnSpeed);

        // Apply the rotation only on the Y-axis
        partToRotate.rotation = Quaternion.Euler(partToRotate.rotation.eulerAngles.x, newYRotation, partToRotate.rotation.eulerAngles.z);

        // Set readyToShoot true
        readyToShoot = true;
    }

    public void SetRange(float range)
    {
        turretRange = range;
    }
}
