using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericGrid<T> : IEnumerable<Vector2Int>
{

    private Dictionary<Vector2Int, T> _dict = new Dictionary<Vector2Int, T>();

    public T this[Vector2Int vect]
    {
        get => _dict[vect];
        set => _dict[vect] = value;
    }

    public T this[int x, int y]
    {
        get => this[new Vector2Int(x, y)];
        set => _dict[new Vector2Int(x, y)] = value;
    }

    public bool ContainsKey(Vector2Int vect) => _dict.ContainsKey(vect);

    public List<T> GetNeighbors(Vector2Int vect)
    {
        List<T> result = new List<T>();

        Vector2Int[] offsets = new Vector2Int[]{
            new Vector2Int(1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(-1, 0),
            new Vector2Int(0, -1)
        };

        foreach (Vector2Int offset in offsets)
        {
            Vector2Int gridPos = vect + offset;
            if (ContainsKey(gridPos))
            {
                result.Add(this[gridPos]);
            }
        }
        return result;
    }

    public IEnumerator<Vector2Int> GetEnumerator()
    {
        foreach (Vector2Int vector in _dict.Keys)
        {
            yield return vector;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    public Vector2Int GetRandomPosition()
    {
        List<Vector2Int> keys = new List<Vector2Int>(_dict.Keys);
        int randomIndex = Random.Range(0, keys.Count);
        return keys[randomIndex];
    }

    public static GenericGrid<T> operator +(GenericGrid<T> grid, Vector2Int vect)
    {
        GenericGrid<T> result = new GenericGrid<T>();
        foreach (Vector2Int position in grid)
        {
            result[position + vect] = grid[position];
        }
        return result;
    }

    public Bounds GetBounds()
    {
        int xMin = int.MaxValue;
        int xMax = int.MinValue;
        int yMin = int.MaxValue;
        int yMax = int.MinValue;
        foreach (Vector2Int relativePosition in this)
        {
            xMin = Mathf.Min(xMin, relativePosition.x);
            xMax = Mathf.Max(xMax, relativePosition.x);
            yMin = Mathf.Min(yMin, relativePosition.y);
            yMax = Mathf.Max(yMax, relativePosition.y);
        }
        return new Bounds(new Vector2Int(xMin, yMin), new Vector2Int(xMax, yMax));
    }

    public GenericGrid<T> GetCenteredGrid()
    {
        Vector2Int center = VectorUtils.ToVector2Int(GetBounds().center);
        return this + (-center);
    }
}

