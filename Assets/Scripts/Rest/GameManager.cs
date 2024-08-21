using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 10;
    [SerializeField]
    private int playersCurrentHealth;
    [SerializeField]
    private int playerMoney = 0;

    // Static flag to control whether saved data should be loaded
    public static bool loadSavedData = false;

    private void Start()
    {
        // Set player's health to max
        playersCurrentHealth = maxHealth;

        // Load saved data only if the flag is set to true
        if (loadSavedData)
        {
            LoadData();
        }
    }

    // Method to apply damage to the player
    public void TakeDamage(int damage)
    {
        playersCurrentHealth -= damage;

        // Check if the player's health has reached zero
        if (playersCurrentHealth <= 0)
        {
            Die();
        }
    }

    // Method to add money to the player's balance
    public void AddMoney(int amount)
    {
        playerMoney += amount;
    }

    // Method to spend money, checks if the player has enough money
    public bool SpendMoney(int amount)
    {
        if (playerMoney >= amount)
        {
            playerMoney -= amount;
            return true;
        }
        return false;
    }

    // Getter for player's current money
    public int PlayerMoney
    {
        get { return playerMoney; }
    }

    // Getter for player's current health
    public int PlayersCurrentHealth
    {
        get { return playersCurrentHealth; }
    }

    // Method called when the player's health reaches zero
    private void Die()
    {
        Debug.Log("Player died. Game Over!");
    }

    // Method to load saved player data
    public void LoadData()
    {
        PlayerData data = SafeSystem.LoadPlayerData();

        if (data != null)
        {
            playerMoney = data.money;
            playersCurrentHealth = data.health;
        }
    }

    // Method to save player data when the application quits
    void OnApplicationQuit()
    {
        SafeSystem.SavePlayerData(this);
    }
}
