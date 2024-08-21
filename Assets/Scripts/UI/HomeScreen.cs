using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeScreen : MonoBehaviour
{
    [SerializeField] 
    private Button playButton;

    [SerializeField]
    private Button resumeGameButton;

    private void Start()
    {
        // Assign the function to the play button
        playButton.onClick.AddListener(StartGame);

        // Assign the function to the resume button
        resumeGameButton.onClick.AddListener(ResumeGame);
    }

    // This function loads the "Map1" scene without loading saved data
    private void StartGame()
    {
        // Ensure that saved data is not loaded
        GameManager.loadSavedData = false;
        
        // Delete saved data before starting a new game
        SafeSystem.DeletePlayerData();

        SceneManager.LoadScene("Map1");
    }

    // This function loads saved data and then loads the "Map1" scene
    private void ResumeGame()
    {
        // Set the flag to load saved data
        GameManager.loadSavedData = true;

        SceneManager.LoadScene("Map1");
    }
}
