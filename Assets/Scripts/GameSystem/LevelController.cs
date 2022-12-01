using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private MazeManager mazeManager;
    [SerializeField] private GameObject playerPrefab;

    private Player player;
    private void Start()
    {
        RunLevel();
    }

    public void RunLevel()
    {
        if (player != null)
            Destroy(player.gameObject);

        mazeManager.RefreshMaze();
        player = Instantiate(playerPrefab).GetComponent<Player>();
        player.SetMaze(mazeManager, this);
        var edges = mazeManager.GraphMaze.PathEdges;
        player.transform.position = edges[0].Begin + new Vector2(mazeManager.transform.position.x, mazeManager.transform.position.y);
    }
}
