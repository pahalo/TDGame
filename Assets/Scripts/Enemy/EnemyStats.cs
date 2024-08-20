using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField]
    private float enemyMovementSpeed = 5f;
    [SerializeField]
    private float enemyMaxHealth = 100f;
    [SerializeField]
    private float enemyCurrentHealth;
    [SerializeField]
    private int bounty = 25;
    [SerializeField]
    private int enemyDamageOnPlayersHealth = 1;


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
    // Function to get the damgae the Enemy will deal the player if it get through
    public int GetEnemyDamageOnPlayersHealth(){
        return enemyDamageOnPlayersHealth;
    }

    // This function will destroy the Enemy after it got no health remaining and after e.g. money and xp was given to the player
    private void Die(){
        GameManager.GetInstance().AddMoney(bounty);
        Destroy(gameObject);
    }
}
