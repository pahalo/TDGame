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

    private GameManager gameManager;
    private EnemyMovement enemyMovement;

    private void Start()
    {
        // Find the GameManager in the scene
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }

        enemyMovement = GetComponent<EnemyMovement>();
        if (enemyMovement == null)
        {
            Debug.LogError("EnemyMovement component missing on the object.");
            return;
        }
        else
        {
            // Set the EnemyMovementSpeed in the EnemyMovement script
            SetEnemyMovementSpeed(enemyMovementSpeed);
        }

        // Set the current health to the max health at the beginning
        enemyCurrentHealth = enemyMaxHealth;
    }

    // Set the EnemyMovementSpeed in the EnemyMovement script
    private void SetEnemyMovementSpeed(float newMovementSpeed)
    {
        enemyMovement.enemyMovementSpeed = newMovementSpeed;
    }

    // Function to apply damage to the enemy
    public void EnemyGetDamage(float damage)
    {
        enemyCurrentHealth -= damage;
        if (enemyCurrentHealth <= 0)
        {
            Die();
        }
    }

    // Function to get the damage the enemy will deal to the player if it gets through
    public int GetEnemyDamageOnPlayersHealth()
    {
        return enemyDamageOnPlayersHealth;
    }

    // This function will destroy the enemy after it has no health remaining and after rewards (e.g., money) are given to the player
    private void Die()
    {
        if (gameManager != null)
        {
            gameManager.AddMoney(bounty);
        }
        Destroy(gameObject);
    }
}
