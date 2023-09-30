using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtils
{
    public static int Modulo(int k, int n)
    {
        int modulo = k %= n;
        return modulo < 0 ? modulo + n : modulo;
    }
}
public static class VectorUtils
{
    public static Vector2Int Rotate(Vector2Int vector, int rotation)
    {
        rotation = MathUtils.Modulo(rotation, 4);
        switch (rotation)
        {
            case 0:
                return vector;
            case 1:
                return new Vector2Int(-vector.y, vector.x);
            case 2:
                return new Vector2Int(-vector.x, -vector.y);
            case 3:
                return new Vector2Int(vector.y, -vector.x);
            default:
                Debug.LogError("Invalid rotation");
                return vector;
        }
    }
}
