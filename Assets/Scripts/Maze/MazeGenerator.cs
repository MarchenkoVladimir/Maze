using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class MazeGenerator
{
    public abstract W4Maze Generate(int width, int height);
}
