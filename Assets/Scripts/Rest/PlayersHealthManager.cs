using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersHealthManager : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 10;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // This Method will be called to deal damage to the player and will check if the player is still alives
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    // GameOverFunction
    private void Die()
    {
        Debug.Log("Player died. Game Over!");
    }
}
