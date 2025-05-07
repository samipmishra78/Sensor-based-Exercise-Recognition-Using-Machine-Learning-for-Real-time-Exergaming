using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Called when the Start button is clicked
    public void StartGame()
    {
        // Load the scene named "game"
        SceneManager.LoadScene("weight");
    }

    // Called when the Quit button is clicked
    public void QuitGame()
    {
        // Quit the application
        // This works in the editor for debugging
        Application.Quit();
    }
}
