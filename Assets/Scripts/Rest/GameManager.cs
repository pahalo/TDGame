using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Static instance of GameManager which allows it to be accessed by any other script.
    private static GameManager instance;

    [SerializeField]
    private int maxHealth = 10;
    [SerializeField]
    private int playersCurrentHealth;
    [SerializeField]
    private int currentPlayerMoney = 0;
    [SerializeField]
    private int playerStartMoney = 200;

    // Static flag to control whether saved data should be loaded
    public static bool loadSavedData = false;
    private string currentMapName;  // The name of the current map

    private EnemySpawner enemySpawner;

    // Ensure there's only one instance of GameManager
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Prevent the GameManager from being destroyed when loading a new scene
            SceneManager.sceneLoaded += OnSceneLoaded; // 
        }
        else if (instance != this)
        {
            Destroy(gameObject);  // Destroy the new GameManager instance if one already exists
            return;
        }
    }
    // Call this method if the scene is beeing changed so the new enemy spawn point is correct
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Aktualisiere den Namen der aktuellen Map
        currentMapName = scene.name;

        if (enemySpawner == null)
        {
            enemySpawner = FindObjectOfType<EnemySpawner>();
        }

        if (enemySpawner != null)
        {
            enemySpawner.InitializeSpawnPoint();
            // Also check if saved data should be loaded
            if (loadSavedData)
            {
                currentMapName = SceneManager.GetActiveScene().name;
                LoadMapData(currentMapName);  // Load data specific to the current map
            }
        }
        else
        {
        Debug.LogError("EnemySpawner not found in the scene.");
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
        currentPlayerMoney += amount;
    }

    // Method to spend money, checks if the player has enough money
    public bool SpendMoney(int amount)
    {
        if (currentPlayerMoney >= amount)
        {
            currentPlayerMoney -= amount;
            return true;
        }
        return false;
    }

    // Getter for player's current money
    public int PlayerMoney
    {
        get { return currentPlayerMoney; }
    }
    // Getter for player's current money
    public int PlayerStartMoney
    {
        get { return playerStartMoney; }
    }

    // Getter for player's current health
    public int PlayersCurrentHealth
    {
        get { return playersCurrentHealth; }
        set { playersCurrentHealth = value; }  // Neuer Setter hinzugefügt
    }
    public int MaxHealth
    {
        get { return maxHealth; }
    }

    // Method called when the player's health reaches zero
    private void Die()
    {
        Debug.Log("Player died. Game Over!");
    }

    // Method to load saved player data
    public void LoadMapData(string mapName)
    {
        MapData data = SafeSystem.LoadMapData(mapName);

        if (data != null)
        {
            currentPlayerMoney = data.money;
            playersCurrentHealth = data.health;
        }
    }

    // Method to save player data when the application quits
    void OnApplicationQuit()
    {
        SavePlayerData();  // Call the method that handles the saving of data
    }

    // Method to save the player data
    private void SavePlayerData()
    {
        SafeSystem.SaveMapData(currentMapName, playersCurrentHealth, currentPlayerMoney);  // Save data specific to the current map
    }

    // Method to save player data when the scene changes
    public void SaveBeforeSceneChange()
    {
        SavePlayerData();  // Save the current data
    }

    // Public method to access the instance of the GameManager
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("GameManager instance is null. Ensure a GameManager exists in the scene.");
            }
            return instance;
        }
    }
}
