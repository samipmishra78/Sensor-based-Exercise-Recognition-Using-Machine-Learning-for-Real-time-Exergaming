using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int Score { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Ensure only one instance exists
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persist this object across scenes
    }

    public void AddScore(int amount)
    {
        Score += amount;
    }

    public void ResetScore()
    {
        Score = 0; // Reset score to zero
    }
}
