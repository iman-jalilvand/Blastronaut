using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false; // Tracking to see if the game is paused?
    public GameObject pauseMenuUI; // Reference to the Pause Menu UI
    void Update()
    {
        // check if the ESC key is pressed
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
        pauseMenuUI.SetActive(false); // Hide the Pause Menu UI
        Time.timeScale = 1f; // Set the game speed to normal -> to resume
        isPaused = false; // The game is no longer paused
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); // Show the Pause Menu UI
        Time.timeScale = 0f; // Freezing the game -> Set the game speed to 0
        isPaused = true; // The game is paused

        // Unlock and show the cursor
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Reset the game speed to normal before laoding the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f; // Reset the game speed to normal before laoding the scene
        SceneManager.LoadScene("MainMenu"); // Load the Main Menu scene
    }
}
