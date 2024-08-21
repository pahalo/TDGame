using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button goHomeButton;

    // This static variable is easily accessible from other scripts to check if the player can click somewhere or more enemies can spawn
    public static bool gameIsPaused = false;

    private void Start()
    {
        // Assign the functions to the buttons
        continueButton.onClick.AddListener(ContinueGame);
        goHomeButton.onClick.AddListener(GoToTheHomeScreen);

        // Ensure the pause menu is inactive at the start
        pauseMenuUI.SetActive(false);
        gameIsPaused = false; // Ensure the game is not paused at the start
    }

    private void Update()
    {
        // Check if the Enter key is pressed
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TogglePauseMenu();
        }
    }

    // This function toggles the pause menu's active state
    public void TogglePauseMenu()
    {
        if (gameIsPaused)
        {
            // Resume the game
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
        }
        else
        {
            // Pause the game
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;
        }
    }

    // This function is called when the Continue button is pressed
    private void ContinueGame()
    {
        TogglePauseMenu();
    }

    private void GoToTheHomeScreen()
    {
    // Saving player data
    SafeSystem.SavePlayerData(FindObjectOfType<GameManager>());

    // Pause time and get back to the Home scren scene
    Time.timeScale = 1f;
    SceneManager.LoadScene("HomeScreen");
}
}
