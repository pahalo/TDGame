using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float enemyMovementSpeed = 5f;

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
    }

    // Set the EnemyMovementSpeed in the EnemyMovement script
    private void SetEnemyMovementSpeed(float newMovementSpeed) {
        enemyMovement.enemyMovementSpeed = newMovementSpeed;
    }
}
