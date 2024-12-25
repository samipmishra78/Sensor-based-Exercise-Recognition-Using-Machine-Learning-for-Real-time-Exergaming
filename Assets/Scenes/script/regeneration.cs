using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> Tiles; // List of tile GameObjects
    private List<Vector3> OriginalTilePositions;     // Store original tile positions
    public static int CurrentTile = 0;              // Static to track the current tile globally

    void Awake()
    {
        // Ensure everything starts fresh on scene load
        InitializeTiles();
    }

    private void InitializeTiles()
    {
        // Store the original positions of all tiles at the start of the game
        OriginalTilePositions = new List<Vector3>();
        foreach (GameObject tile in Tiles)
        {
            OriginalTilePositions.Add(tile.transform.position);
        }

        // Reset the tile positions to their original state
        ResetTiles();
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

        Debug.Log("Tiles initialized to original positions.");
    }

    private void OnTriggerEnter(Collider other)
    {
        int randomTile;

        // Generate a random tile to move
        if (CurrentTile == Tiles.Count - 1)
        {
            randomTile = Random.Range(0, Tiles.Count - 1);
        }
        else
        {
            randomTile = Random.Range(CurrentTile + 1, Tiles.Count);
        }

        // Move the new tile to the correct position
        Tiles[randomTile].transform.position = new Vector3(
            Tiles[CurrentTile].transform.position.x + 192,
            0,
            0
        );

        // Update the current tile index
        CurrentTile = randomTile;
    }
}
