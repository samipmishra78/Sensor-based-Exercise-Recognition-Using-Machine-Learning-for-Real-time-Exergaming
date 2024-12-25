using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    private Vector3[] initialPositions;
    private Transform[] tiles;

    void Start()
    {
        // Cache the initial positions of all tiles at the start
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
        // Reset the tile positions
        if (tiles != null && initialPositions != null)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i].position = initialPositions[i];
            }
        }

        SceneManager.LoadScene("game"); // Replace "game" with your actual scene name if it's different
    }

    public void QuitToMenu()
    {
        // Load the Menu Scene
        SceneManager.LoadScene("mainmenu");
    }
}
