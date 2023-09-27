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
