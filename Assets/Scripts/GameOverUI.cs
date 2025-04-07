using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverUI : MonoBehaviour
{

    public TextMeshProUGUI scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Unlock and show cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Show the final score
        int finalScore = GameManager.Instance.GetFinalScore();
        scoreText.text = "Score: " + finalScore;
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("Tutorial"); // Replace with your game scene name
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
