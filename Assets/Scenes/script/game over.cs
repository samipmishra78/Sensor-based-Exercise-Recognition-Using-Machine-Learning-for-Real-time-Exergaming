using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // For UI elements

public class GameOverManager : MonoBehaviour
{
    public Text scoreText; // Reference to the UI Text element to display the score

    private void Start()
    {
        // Display the score on the Game Over screen
        scoreText.text = "Score: " + ScoreManager.Instance.Score;
    }

    public void RetryGame()
    {
        // Reset the score
        ScoreManager.Instance.ResetScore();

        // Load the main game scene
        SceneManager.LoadScene("game");
    }
    public void menu()
    {
        SceneManager.LoadScene("mainmenu");
    }
}
