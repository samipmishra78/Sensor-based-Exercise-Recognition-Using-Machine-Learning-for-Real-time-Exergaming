using UnityEngine;
using TMPro;

public class Scores : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;  // UI element for the current score display during gameplay

    [SerializeField]
    private TextMeshProUGUI finalScoreText;  // UI element for displaying the final score in the Game Over menu

    [SerializeField]
    private GameObject gameOverMenu;  // Game Over menu panel

    private int score = 0;  // Variable to hold the score
    private float timer = 0f;  // Timer to control score updates
    public float scoreInterval = 1f;  // Time interval for score increment
    private bool isStopped = false;  // Flag to stop score updates

    void Start()
    {
        ResetGame();  // Initialize the game state
    }

    void Update()
    {
        // Only update score if the game is not stopped
        if (!isStopped)
        {
            // Increment the timer
            timer += Time.deltaTime;

            // Check if the timer has reached the interval
            if (timer >= scoreInterval)
            {
                score += 1;  // Increment the score
                timer = 0f;  // Reset the timer
                UpdateScoreText();  // Update the score on the UI
            }
        }
    }

    // Updates the current score in the gameplay UI
    private void UpdateScoreText()
    {
        scoreText.text = "SCORE: " + score.ToString();
    }

    // Handles collision and stops the score update
    private void OnTriggerEnter(Collider other)
    {
        // Replace "YourObjectTag" with the tag of the object to detect
        if (other.CompareTag("obs"))
        {
            isStopped = true;  // Stop the score increment
            ShowGameOverMenu();  // Display the Game Over menu
        }
    }

    // Displays the Game Over menu and updates the final score
    private void ShowGameOverMenu()
    {
        gameOverMenu.SetActive(true);  // Show the Game Over menu
        finalScoreText.text = "FINAL SCORE: " + score.ToString();  // Display the final score
    }

    // Resets the game state and hides the Game Over menu
    public void ResetGame()
    {
        score = 0;  // Reset the score
        timer = 0f;  // Reset the timer
        isStopped = false;  // Allow score updates
        UpdateScoreText();  // Update the UI with the reset score
        gameOverMenu.SetActive(false);  // Hide the Game Over menu
    }
}
