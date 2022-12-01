using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    [SerializeField] private GameObject _CellPrefab;
    [SerializeField] private int _MazeCellsX = 5;
    [SerializeField] private int _MazeCellsY = 5;
    
    [SerializeField] private bool _IsDrawGizmos;

    private List<GameObject> _Cells = new List<GameObject>();
    public List<GameObject> Cells => _Cells;
    private W4Maze _Maze;
    private MazeGraph _GraphMaze;
    public MazeGraph GraphMaze => _GraphMaze;
    public void RefreshMaze()
    {
        if(transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        _Cells.Clear();

        var generator = new EllerGenerator();
        _Maze = generator.Generate(_MazeCellsX, _MazeCellsY);
        _GraphMaze = new MazeGraph(_Maze, true);
        var mazeGO = GenerateW4MazeMesh(_Maze);

        for (int i = 0; i < _Cells.Count; i++)
        {
            _Cells[i].transform.SetParent(mazeGO.transform.GetChild(0));
        }
       
    }
    public GameObject GenerateW4MazeMesh(W4Maze maze)
    {
        var mazeGO = new GameObject();
        var mazeWidth = maze.ColumnCount;
        var mazeDepth = maze.RowCount;

        var wallsGO = CreateWalls(new MazeGraph(maze, true));

#if UNITY_EDITOR
        mazeGO.transform.SetParent(transform);
        wallsGO.transform.SetParent(mazeGO.transform);
        mazeGO.name = "Maze";
        wallsGO.name = "Cells";
#endif
        return mazeGO;
    }

    private GameObject CreateWalls(MazeGraph maze)
    {
        var wallsGO = new GameObject();
        var cells = _GraphMaze.Cells;
        for (int i = 0; i < cells.Count; i++)
        {
            var cell = Instantiate(_CellPrefab);
            cell.transform.position = cells[i].Position + new Vector2(transform.position.x, transform.position.y);
            Cell c = cell.GetComponent<Cell>();
            c.SetWall(
                cells[i].W4Cell.RightWall,
                cells[i].W4Cell.LeftWall,
                cells[i].W4Cell.TopWall,
                cells[i].W4Cell.BotWall);

            if (i + 1 == cells.Count) c.SetFinish();

            _Cells.Add(cell);
        }
        return wallsGO;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (_IsDrawGizmos)
        {
            if (_Maze != null)
            {
                Gizmos.color = Color.green;
                UnityEditor.Handles.color = Color.white;
                var edges = _GraphMaze.PathEdges;
                Vector2 thisPosition = new Vector2(transform.position.x, transform.position.y);
                for (int i = 0; i < edges.Count; i++)
                {
                    Gizmos.DrawLine(edges[i].Begin + thisPosition, edges[i].End + thisPosition);
                    UnityEditor.Handles.DrawSolidDisc(edges[i].Begin + thisPosition, -Vector3.forward, 0.05f);
                    UnityEditor.Handles.DrawSolidDisc(edges[i].End + thisPosition, -Vector3.forward, 0.05f);
                }
                Gizmos.color = Color.yellow;
                Handles.color = Color.white;
                edges = _GraphMaze.WallEdges;
                for (int i = 0; i < edges.Count; i++)
                {
                    Gizmos.DrawLine(edges[i].Begin + thisPosition, edges[i].End + thisPosition);
                    UnityEditor.Handles.DrawSolidDisc(edges[i].Begin + thisPosition, -Vector3.forward, 0.05f);
                    UnityEditor.Handles.DrawSolidDisc(edges[i].End + thisPosition, -Vector3.forward, 0.05f);
                }
            }
        }
    }
#endif
}
