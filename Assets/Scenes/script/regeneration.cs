using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> Tiles;
    int RandomTile;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (player.CurrentTile == Tiles.Capacity - 1)
        {
            RandomTile = Random.Range(0, Tiles.Capacity - 1);
        }
        else
            RandomTile = Random.Range(player.CurrentTile + 1, Tiles.Capacity);
        Tiles[RandomTile].transform.position = new Vector3(Tiles[player.CurrentTile].transform.position.x + 192,0, 0);
        player.CurrentTile = RandomTile;
    }
}//Tiles[player.CurrentTile].transform.position.y