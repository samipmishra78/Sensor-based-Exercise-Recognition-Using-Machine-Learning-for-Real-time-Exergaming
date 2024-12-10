using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; } // Singleton instance

    private int currentScore = 0; // Current score

    private void Awake()
    {
        // Ensure only one instance of ScoreManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Gets the current score
    public int GetCurrentScore()
    {
        return currentScore;
    }

    // Adds to the current score
    public void AddScore(int amount)
    {
        currentScore += amount;
    }

    // Resets the score to zero
    public void ResetScore()
    {
        currentScore = 0;
    }
}
