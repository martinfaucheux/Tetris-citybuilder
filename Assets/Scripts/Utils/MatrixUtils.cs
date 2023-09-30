using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MatrixUtils
{
    public static List<Vector2Int> GetNeighbors_8()
    {
        List<Vector2Int> neighbors = new List<Vector2Int>{
            Vector2Int.up,
            (Vector2Int.down),
            (Vector2Int.left),
            (Vector2Int.right),
            (Vector2Int.up + Vector2Int.left),
            (Vector2Int.up + Vector2Int.right),
            (Vector2Int.down + Vector2Int.left),
            (Vector2Int.down + Vector2Int.right)
         };

        return neighbors;
    }

    public static List<Vector2Int> GetNeighbors_4()
    {
        List<Vector2Int> neighbors = new List<Vector2Int>{
            (Vector2Int.up),
            (Vector2Int.down),
            (Vector2Int.left),
            (Vector2Int.right)
         };

        return neighbors;
    }
}

public struct Bounds
{
    public Vector2Int min;
    public Vector2Int max;
    public Bounds(Vector2Int min, Vector2Int max)
    {
        this.min = min;
        this.max = max;
    }

    public Vector2 size { get => new Vector2(1 + max.x - min.x, 1 + max.y - min.y); }
    public Vector2 center { get => new Vector2((max.x + min.x) / 2f, (max.y + min.y) / 2f); }
}