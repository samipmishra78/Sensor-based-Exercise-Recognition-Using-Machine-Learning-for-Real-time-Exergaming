using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void RetryGame()
    {
     /*   // Reset the score (if you have a ScoreManager)
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();
        }*/

        // Reset the tile positions
        TrackManager trackManager = FindObjectOfType<TrackManager>();
        if (trackManager != null)
        {
            trackManager.ResetTiles(); // Reset the tiles to their original positions
        }

        // Reload the Game Scene
        SceneManager.LoadScene("game"); // Replace "game" with your actual scene name if it's different
    }

    public void QuitToMenu()
    {
        // Reset the score 
        /*
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();
        }*/

        // Load the Menu Scene
        SceneManager.LoadScene("mainmenu");
    }
}
