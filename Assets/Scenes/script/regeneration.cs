using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> Tiles; // List of tile GameObjects
    private List<Vector3> OriginalTilePositions;     // Store original tile positions
    private int RandomTile;                          // Declare RandomTile as a class-level variable
    public static int CurrentTile = 0;              // Static to track the current tile globally

    void Start()
    {
        // Store the original positions of the tiles at the start of the game
        OriginalTilePositions = new List<Vector3>();
        foreach (GameObject tile in Tiles)
        {
            OriginalTilePositions.Add(tile.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Generate a random tile to move
        if (CurrentTile == Tiles.Count - 1)
        {
            RandomTile = Random.Range(0, Tiles.Count - 1);
        }
        else
        {
            RandomTile = Random.Range(CurrentTile + 1, Tiles.Count);
        }

        // Move the new tile to the correct position with z-axis set to 0.3
        Tiles[RandomTile].transform.position = new Vector3(
            Tiles[CurrentTile].transform.position.x + 192, // Adjust x-axis by +192
            Tiles[RandomTile].transform.position.y,        // Keep y-axis unchanged
            0.3f                                           // Set z-axis to 0.3
        );

        // Update the current tile index
        CurrentTile = RandomTile;
    }

    public void ResetTiles()
    {
        // Reset all tiles to their original positions
        for (int i = 0; i < Tiles.Count; i++)
        {
            Tiles[i].transform.position = OriginalTilePositions[i];
        }

        // Reset the current tile index
        CurrentTile = 0;

        Debug.Log("Tiles reset to original positions.");
    }
}
