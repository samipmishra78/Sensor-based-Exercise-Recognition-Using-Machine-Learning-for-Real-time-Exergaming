using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI statsText;
    public TextMeshProUGUI calorieBurnText; // UI for Calories Burned

    private Vector3[] initialPositions;
    private Transform[] tiles;

    void Start()
    {
        // Calculate Calories Burned
        GameStats.CalculateCaloriesBurned();

        if (statsText != null)
        {
            statsText.text = $"Jumps: {GameStats.jumpCount}\n" +
                             $"Squats (Slides): {GameStats.slideCount}\n" +
                             $"Jogging Time: {GameStats.joggingTime:F2} seconds\n" +
                             $"Final Score: {GameStats.finalScore}";
        }

        if (calorieBurnText != null)
        {
            calorieBurnText.text = $"Calories Burned: {GameStats.caloriesBurned:F2} kcal";
        }

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

    public void QuitToMenu()
    {
        SceneManager.LoadScene("mainmenu");
    }
}
