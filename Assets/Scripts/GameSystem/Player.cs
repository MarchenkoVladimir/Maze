using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private MazeManager mazeManager;
    [SerializeField] private float spedMove = 1f;
    private float timeHold;
    private bool isHolding;
    private bool isMoving;
    private Vector2 startPos;
    private Vector2 targetPosition;
    private Vector2 previosPosition;
    private LevelController levelController;

    public void SetMaze(MazeManager manager, LevelController controller)
    {
        mazeManager = manager;
        levelController = controller;
        targetPosition = transform.position;
    }
    private void OnEnable()
    {
        InputHandler.OnStartHold += OnDrag;
        InputHandler.OnStopHold += OnEndHold;
    }

    Vector2 refFaktor;
    private void Update()
    {
        if (isHolding)
            timeHold += Time.deltaTime;
    }

    private void OnDisable()
    {
        InputHandler.OnStartHold -= OnDrag;
        InputHandler.OnStopHold -= OnEndHold;
    }
    public void OnDrag(PointerEventData eventData)
    {
        startPos = eventData.position;
        isHolding = true;
    }

    public void OnEndHold(PointerEventData eventData)
    {
        isHolding = false;
        if (timeHold >= 0.5f || isMoving)
        {
            Debug.Log("time out or moving player");
            timeHold = 0;
            return;
        }

        var delta = eventData.position - startPos;
        var X = Mathf.Abs(delta.x);
        var Y = Mathf.Abs(delta.y);

        var edges = mazeManager.GraphMaze.PathEdges;
        Vector2 mazePosition = new Vector2(mazeManager.transform.position.x, mazeManager.transform.position.y);
        var paths = edges.FindAll(x => x.Begin + mazePosition == new Vector2(transform.position.x, transform.position.y));
        var pathsEnd = edges.FindAll(x => x.End + mazePosition == new Vector2(transform.position.x, transform.position.y));
        if (X > Y)
        {
            if (delta.x > 0)
            {
                if (paths.Count != 0)
                {
                    if (paths.Any(x => x.End.x + mazePosition.x > transform.position.x))
                    {
                        var f = paths.Find(x => x.End.x + mazePosition.x > transform.position.x);
                        targetPosition = f.End + mazePosition;
                       StartCoroutine(MovePlaeyr(targetPosition));
                    }
                    else if (pathsEnd.Count != 0)
                    {
                        if (pathsEnd.Any(x => x.Begin.x + mazePosition.x > transform.position.x))
                        {
                            var f = pathsEnd.Find(x => x.Begin.x + mazePosition.x > transform.position.x);
                            targetPosition = f.Begin + mazePosition;
                            StartCoroutine(MovePlaeyr(targetPosition));
                        }
                    }
                }
                else if (pathsEnd.Count != 0)
                {
                    if (pathsEnd.Any(x => x.Begin.x + mazePosition.x > transform.position.x))
                    {
                        var f = pathsEnd.Find(x => x.Begin.x + mazePosition.x > transform.position.x);
                        targetPosition = f.Begin + mazePosition;
                        StartCoroutine(MovePlaeyr(targetPosition));
                    }
                }
            }
            else
            {
                if (paths.Count != 0)
                {
                    if (paths.Any(x => x.End.x + mazePosition.x < transform.position.x))
                    {
                        var f = paths.Find(x => x.End.x + mazePosition.x < transform.position.x);
                        targetPosition = f.End + mazePosition;
                        StartCoroutine(MovePlaeyr(targetPosition));
                    }
                    else if (pathsEnd.Count != 0)
                    {
                        if (pathsEnd.Any(x => x.Begin.x + mazePosition.x < transform.position.x))
                        {
                            var f = pathsEnd.Find(x => x.Begin.x + mazePosition.x < transform.position.x);
                            targetPosition = f.Begin + mazePosition;
                            StartCoroutine(MovePlaeyr(targetPosition));
                        }
                    }
                }
                else if (pathsEnd.Count != 0)
                {
                    if (pathsEnd.Any(x => x.Begin.x + mazePosition.x < transform.position.x))
                    {
                        var f = pathsEnd.Find(x => x.Begin.x + mazePosition.x < transform.position.x);
                        targetPosition = f.Begin + mazePosition;
                        StartCoroutine(MovePlaeyr(targetPosition));
                    }
                }

            }
        }
        else if (X < Y)
        {
            if (delta.y > 0)
            {
                if (paths.Count != 0)
                {
                    if (paths.Any(x => x.End.y + mazePosition.y > transform.position.y))
                    {
                        var f = paths.Find(x => x.End.y + mazePosition.y > transform.position.y);
                        targetPosition = f.End + mazePosition;
                        StartCoroutine(MovePlaeyr(targetPosition));
                    }
                    else if (pathsEnd.Count != 0)
                    {
                        if (pathsEnd.Any(x => x.Begin.y + mazePosition.y > transform.position.y))
                        {
                            var f = pathsEnd.Find(x => x.Begin.y + mazePosition.y > transform.position.y);
                            targetPosition = f.Begin + mazePosition;
                            StartCoroutine(MovePlaeyr(targetPosition));
                        }
                    }
                }
                else if (pathsEnd.Count != 0)
                {
                    if (pathsEnd.Any(x => x.Begin.y + mazePosition.y > transform.position.y))
                    {
                        var f = pathsEnd.Find(x => x.Begin.y + mazePosition.y > transform.position.y);
                        targetPosition = f.Begin + mazePosition;
                        StartCoroutine(MovePlaeyr(targetPosition));
                    }
                }
            }
            else
            {
                if (paths.Count != 0)
                {
                    if (paths.Any(x => x.End.y + mazePosition.y < transform.position.y))
                    {
                        var f = paths.Find(x => x.End.y + mazePosition.y < transform.position.y);
                        targetPosition = f.End + mazePosition;
                        StartCoroutine(MovePlaeyr(targetPosition));
                    }
                    else if (pathsEnd.Count != 0)
                    {
                        if (pathsEnd.Any(x => x.Begin.y + mazePosition.y < transform.position.y))
                        {
                            var f = pathsEnd.Find(x => x.Begin.y + mazePosition.y < transform.position.y);
                            targetPosition = f.Begin + mazePosition;
                            StartCoroutine(MovePlaeyr(targetPosition));
                        }
                    }
                }
                else if (pathsEnd.Count != 0)
                {
                    if (pathsEnd.Any(x => x.Begin.y + mazePosition.y < transform.position.y))
                    {
                        var f = pathsEnd.Find(x => x.Begin.y + mazePosition.y < transform.position.y);
                        targetPosition = f.Begin + mazePosition;
                        StartCoroutine(MovePlaeyr(targetPosition));
                    }
                }
            }
        }

        timeHold = 0;
    }

    public IEnumerator MovePlaeyr(Vector2 pos)
    {
        isMoving = true;
        float totalMovementTime = 0.7f;
        float currentMovementTime = 0f;
        previosPosition = transform.position;
        while (Vector3.Distance(transform.position, pos) > 0)
        {
            currentMovementTime += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, pos, currentMovementTime / totalMovementTime);
            yield return null;
        }
        
        if (Vector3.Distance(mazeManager.Cells[mazeManager.Cells.Count -1].transform.position, pos) < 0.1f)
        {
            levelController.RunLevel();
            Debug.Log("Finish");
        }
        transform.position = pos;
        isMoving = false;

        var edges = mazeManager.GraphMaze.PathEdges;
        Vector2 mazePosition = new Vector2(mazeManager.transform.position.x, mazeManager.transform.position.y);
        var paths = edges.FindAll(x => x.Begin + mazePosition == new Vector2(transform.position.x, transform.position.y));
        var pathsEnd = edges.FindAll(x => x.End + mazePosition == new Vector2(transform.position.x, transform.position.y));

        if(paths.Count + pathsEnd.Count < 3)
        {
            if(paths.Count != 0)
            {
                if (paths.Any(x => x.End + mazePosition != previosPosition))
                {
                    var f = paths.Find(x => x.End + mazePosition != previosPosition);
                    targetPosition = f.End + mazePosition;
                    StartCoroutine(MovePlaeyr(targetPosition));
                }
                else if (pathsEnd.Count != 0)
                {
                    if (pathsEnd.Any(x => x.Begin + mazePosition != previosPosition))
                    {
                        var f = pathsEnd.Find(x => x.Begin + mazePosition != previosPosition);
                        targetPosition = f.Begin + mazePosition;
                        StartCoroutine(MovePlaeyr(targetPosition));
                    }

                }

            }
            else if (pathsEnd.Count != 0)
            {
                if (pathsEnd.Any(x => x.Begin + mazePosition != previosPosition))
                {
                    var f = pathsEnd.Find(x => x.Begin + mazePosition != previosPosition);
                    targetPosition = f.Begin + mazePosition;
                    StartCoroutine(MovePlaeyr(targetPosition));
                }
                else if(paths.Count != 0)
                {
                    if (paths.Any(x => x.End + mazePosition != previosPosition))
                    {
                        var f = paths.Find(x => x.End + mazePosition != previosPosition);
                        targetPosition = f.End + mazePosition;
                        StartCoroutine(MovePlaeyr(targetPosition));
                    }
                }
                    
            }
        }
       
    }


}
