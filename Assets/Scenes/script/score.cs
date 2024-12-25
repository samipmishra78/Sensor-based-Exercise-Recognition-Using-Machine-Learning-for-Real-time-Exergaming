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

    public static int finalScore = 0;  // Static variable to store the score for the Game Over screen

    void Start()
    {
        UpdateScoreText();  // Update the UI at the start
    }

    void Update()
    {
        // Only update score if not stopped
        if (!isStopped)
        {
            // Check if the player is pressing W, S, or Space
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Space))
            {
                // Accumulate time
                timer += Time.deltaTime;

                // Check if the timer has reached the score interval
                if (timer >= scoreInterval)
                {
                    score += 1;  // Increase score by 1
                    GameManagers.currentScore = score;  // Access via class name, since currentScore is static
                    timer = 0f;  // Reset timer
                    UpdateScoreText();  // Update the displayed score
                }
            }
        }
    }

    // Function to update the score text on the UI
    private void UpdateScoreText()
    {
        scoreText.text = "SCORE: " + score.ToString();
    }

    // Collision detection to stop score updating
    private void OnTriggerEnter(Collider other)
    {
        // If the player collides with an obstacle, stop the score update and trigger Game Over
        if (other.CompareTag("obs"))
        {
            isStopped = true;  // Stop score updating
            finalScore = score;  // Store the final score for the Game Over screen
            GameManagers.instance.SetGameOver();  // Call the Game Over function to switch scenes
        }
    }
}
