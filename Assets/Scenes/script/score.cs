using UnityEngine;
using TMPro;

public class scores : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;  // Reference to the TextMeshProUGUI component for displaying the score

    private int score = 0;  // Integer to hold the score
    private float timer = 0f;  // Timer to control when to increment score
    public float scoreInterval = 1f;  // Interval in seconds for score increment
    private bool isStopped = false;  // Flag to stop score updating

    void Start()
    {
        UpdateScoreText();  // Update the UI at the start
    }

    void Update()
    {
        // Only update score if not stopped
        if (!isStopped)
        {
            // Accumulate time
            timer += Time.deltaTime;

            // Check if the timer has reached the score interval
            if (timer >= scoreInterval)
            {
                score += 1;  // Increase score by 1
                timer = 0f;  // Reset timer
                UpdateScoreText();  // Update the displayed score
            }
        }
    }

    // Function to update the score text on the UI
    private void UpdateScoreText()
    {
        scoreText.text = "SCORE: " + score.ToString();
    }

    // Optional: Method to add a specific amount to the score
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    // Collision detection to stop score updating
    private void OnTriggerEnter(Collider other)
    {
        // Replace "YourObjectTag" with the tag of the object you want to detect
        if (other.CompareTag("YourObjectTag"))
        {
            isStopped = true;  // Stop score updating on collision
        }
    }
}
