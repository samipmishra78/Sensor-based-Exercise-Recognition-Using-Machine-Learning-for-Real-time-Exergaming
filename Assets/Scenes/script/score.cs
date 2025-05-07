using UnityEngine;
using TMPro;

public class Scores : MonoBehaviour
{
    private int score = 0;  // Integer to hold the score
    private float timer = 0f;  // Timer for score increment
    public float scoreInterval = 1f;  // Time interval for scoring
    private bool isStopped = false;  // Flag to stop scoring

    public TextMeshProUGUI scoreText; // Assign this in the Inspector

    void Start()
    {
        UpdateScoreText();
    }

    void Update()
    {
        if (!isStopped)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Space))
            {
                timer += Time.deltaTime;

                if (timer >= scoreInterval)
                {
                    score += 1;
                    GameStats.finalScore = score;  // Store in GameStats
                    timer = 0f;
                    UpdateScoreText();
                }
            }
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "SCORE: " + score.ToString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("obs"))
        {
            isStopped = true;
            GameStats.finalScore = score;  // Save final score
            GameStats.CalculateCaloriesBurned(); // Calculate calories
            GameManagers.instance.SetGameOver(); // Trigger Game Over
        }
    }
}
