using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int finalScore = 0; // Store score between scenes

    public static GameManager Instance; // Singleton pattern

    private int score = 0; // Score variable
    public TextMeshProUGUI scoreText; // Reference to the UI text

    void Awake()
    {
        // Singleton pattern to ensure there's only one GameManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreUI(); // Initialize score display
    }

    // Method to increment the score
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI(); // Ensure the UI is updated after score changes
    }

    // Method to update the UI text
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void SaveFinalScore()
    {
        finalScore = score;
    }

    public int GetFinalScore()
    {
        return finalScore;
    }
}