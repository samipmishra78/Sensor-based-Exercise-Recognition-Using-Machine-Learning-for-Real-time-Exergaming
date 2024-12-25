using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagers : MonoBehaviour
{
    public static int currentScore; // Static variable to store the score

    public static GameManagers instance;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of GameManagers exists
        if (instance == null)
        {
            instance = this;
        }
    }

    // Method to set the game over and switch to the Game Over scene
    public void SetGameOver()
    {
        // You can add logic here, such as saving the score, showing a game over screen, etc.
        Debug.Log("Game Over! Final score: " + currentScore);
        SceneManager.LoadScene("gameover"); // Switch to game over scene
    }

    // Other methods or logic for GameManagers...
}
