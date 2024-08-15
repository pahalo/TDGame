using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public float damage; // Damage this bullet inflicts
    public float bulletSpeed = 3f;

    private Transform target; // Target the bullet is homing towards
    
    // move towawrds the target
    void Update()
    {
        if (target != null)
        {
            // Calculate the direction to the target and move towards it
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * bulletSpeed * Time.deltaTime;
        }
        else
        {
            // Optionally keep moving forward if no target is set
            DestroyBullet();
        }
    }


    // Method to set the damage the bullet deals
    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    // Method to set the target so the bullet knows who to follow
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Get the EnemyStats script to deal damage
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                // Apply damage to the enemy
                enemyStats.EnemyGetDamage(damage);
            }
            DestroyBullet();
        }
    }

    private void DestroyBullet(){   
        Destroy(gameObject); // Destroy the bullet upon hitting
    }
}
