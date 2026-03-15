using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI; // Reference to the pause menu UI
    private bool isPaused = false; // Flag to track if the game is currently paused
    void Start()
    {
        pauseMenuUI.SetActive(false); // Ensure the pause menu is hidden at the start
    }
    void Update()
    {
        // Check for the Escape key press to toggle the pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume(); // If the game is paused, resume it
            }
            else
            {
                Pause(); // If the game is not paused, pause it
            }
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor back to the center of the screen
        Cursor.visible = false; // Hide the cursor
        pauseMenuUI.SetActive(false); // Hide the pause menu UI
        Time.timeScale = 1f; // Resume the game by setting time scale to normal
        isPaused = false; // Update the paused state
    }

    public void Pause()
    {

        Cursor.lockState = CursorLockMode.None; // Unlock the cursor so the player can interact with the pause menu
        Cursor.visible = true; // Make the cursor visible
        pauseMenuUI.SetActive(true); // Show the pause menu UI
        Time.timeScale = 0f; // Pause the game by setting time scale to zero
        isPaused = true; // Update the paused state
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Ensure the game is not paused when loading the main menu
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu"); // Load the main menu scene
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit the application
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // Ensure the game is not paused when restarting the level
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name); // Reload the current scene
    }
}
