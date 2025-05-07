using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI statsText;
    public TextMeshProUGUI calorieBurnText; // UI for Calories Burned
    public TextMeshProUGUI scoreDisplayText; // UI for Final Score

    private Vector3[] initialPositions;
    private Transform[] tiles;

    void Start()
    {
        // Calculate Calories Burned
        GameStats.CalculateCaloriesBurned();

        // Divide final score by 800 and display it
        int finalScoreQuotient = GameStats.finalScore / 800;

        // Display final score
        if (scoreDisplayText != null)
        {
            scoreDisplayText.text = "FINAL SCORE: " + finalScoreQuotient.ToString();
        }

        // Display other stats
        if (statsText != null)
        {
            statsText.text = $"Jumps: {GameStats.jumpCount}\n" +
                             $"Squats (Slides): {GameStats.slideCount}\n" +
                             $"Jogging Time: {GameStats.joggingTime:F2} seconds\n" +
                             $"Final Score: {finalScoreQuotient}";  // Display the quotient value
        }

        // Display Calories Burned
        if (calorieBurnText != null)
        {
            calorieBurnText.text = $"Calories Burned: {GameStats.caloriesBurned:F2} cal";
        }

        // Reset tiles positions if necessary (for retry functionality)
        TrackManager trackManager = FindObjectOfType<TrackManager>();
        if (trackManager != null)
        {
            tiles = trackManager.GetComponentsInChildren<Transform>();
            initialPositions = new Vector3[tiles.Length];

            for (int i = 0; i < tiles.Length; i++)
            {
                initialPositions[i] = tiles[i].position;
            }
        }
    }

    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitToMenu();
        }
    }

    // Method to retry the game and reset the tiles and stats
    public void RetryGame()
    {
        if (tiles != null && initialPositions != null)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i].position = initialPositions[i];
            }
        }

        GameStats.ResetStats();
        SceneManager.LoadScene("game");
    }

    // Method to quit to the main menu
    public void QuitToMenu()
    {
        SceneManager.LoadScene("mainmenu");
    }
}
