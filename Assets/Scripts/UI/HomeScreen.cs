using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeScreen : MonoBehaviour
{
    public enum MapName
    {
        Map1,
        Map2
    }

    [SerializeField] 
    private Button playButton;
    [SerializeField] 
    private Button playButton2;

    [SerializeField]
    private Button resumeGameButton;
    [SerializeField]
    private Button resumeGameButton2;

    [SerializeField]
    private MapName map1 = MapName.Map1;

    [SerializeField]
    private MapName map2 = MapName.Map2;

    private void Start()
    {
        // Assign the function to the play button
        playButton.onClick.AddListener(() => StartGame(map1));

        // Assign the function to the play button2
        playButton2.onClick.AddListener(() => StartGame(map2));

        // Assign the function to the resume button
        resumeGameButton.onClick.AddListener(() => ResumeGame(map1));

        // Assign the function to the resume button2
        resumeGameButton2.onClick.AddListener(() => ResumeGame(map2));
    }

    // This function loads a scene without loading saved data
    private void StartGame(MapName mapName)
    {
        // Ensure that saved data is not loaded
        GameManager.loadSavedData = false;
    
        // Get the current amount of money the player has
        int currentMoney = GameManager.Instance.PlayerMoney;

        // Spend all the current money to set the player's money to 0
        GameManager.Instance.SpendMoney(currentMoney);

        // Add the player's starting money
        int startingMoney = GameManager.Instance.PlayerStartMoney;
        GameManager.Instance.AddMoney(startingMoney);

        // Set the player's health to maximum using the MaxHealth property
        GameManager.Instance.PlayersCurrentHealth = GameManager.Instance.MaxHealth;

        // Delete saved data for the specified map before starting a new game
        SafeSystem.DeleteMapData(mapName.ToString());

        // Load the scene based on the enum value
        SceneManager.LoadScene(mapName.ToString());
    }

    
    // This function loads saved data and then loads the specified scene
    private void ResumeGame(MapName mapName)
    {
        // Set the flag to load saved data
        GameManager.loadSavedData = true;

        // Load the scene based on the enum value
        SceneManager.LoadScene(mapName.ToString());
    }
}
