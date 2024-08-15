using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float enemyMovementSpeed = 5f;
    public float enemyMaxHealth = 100f;
    public float enemyCurrentHealth;

    private EnemyMovement enemyMovement;

    private void Start(){
        enemyMovement = GetComponent<EnemyMovement>();
        if (enemyMovement == null)
        {
            Debug.LogError("EnemyMovement component missing on the object.");
            return;
        } else {
            // Set the EnemyMovment variable "EnemyMovementSpeed"
            SetEnemyMovementSpeed(enemyMovementSpeed);
        }

        // Setting Variables to their max Value in the beginning
        enemyCurrentHealth = enemyMaxHealth;
    }

    // Set the EnemyMovementSpeed in the EnemyMovement script
    private void SetEnemyMovementSpeed(float newMovementSpeed) {
        enemyMovement.enemyMovementSpeed = newMovementSpeed;
    }

    // Function to get damage
    public void EnemyGetDamage(float damage){
        enemyCurrentHealth = enemyCurrentHealth - damage;
        if(enemyCurrentHealth <= 0) {
            Die();
        }
    }
    // This function will destroy the Enemy after it got no health remaining and after e.g. money and xp was given to the player
    private void Die(){
        Destroy(gameObject);
    }
}
