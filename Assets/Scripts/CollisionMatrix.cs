using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionMatrix : Singleton<CollisionMatrix>
{
    public int maxX = 5;
    public Vector3 origin;

    private List<MatrixCollider> colliderList = new List<MatrixCollider>();


    public void AddCollider(MatrixCollider collider)
    {
        colliderList.Add(collider);
    }

    public void RemoveCollider(MatrixCollider collider)
    {
        colliderList.Remove(collider);
    }


    public List<GameObject> GetObjectsAtPosition(Vector2Int matrixPosition)
    {
        return GetCollidersAtPosition(matrixPosition).Select(col => col.gameObject).ToList();
    }

    public List<MatrixCollider> GetCollidersAtPosition(Vector2Int matrixPosition)
    {
        List<MatrixCollider> result = new List<MatrixCollider>();
        foreach (MatrixCollider collider in colliderList)
        {
            if (collider.matrixPosition == matrixPosition)
                result.Add(collider);
        }
        return result;
    }

    public bool IsValidPosition(Vector2Int matrixPosition)
    {
        int x = matrixPosition.x;
        int y = matrixPosition.y;

        return (y >= 0 && Mathf.Abs(x) <= maxX); 
    }

    public Vector2Int GetMatrixPos(Transform transform)
    {
        Vector3 realPos = transform.position - origin;
        float x = realPos.x;
        float y = realPos.y;
        return new Vector2Int((int)x, (int)y);
    }


    public Vector3 GetRealWorldPosition(Vector2Int matrixPos)
    {
        float x = matrixPos.x;
        float y = matrixPos.y;
        Vector3 realWorldPos;
        realWorldPos = new Vector3(x, y, 0);
        return origin + realWorldPos;
    }

    public Vector3 GetRealWorldVector(Direction direction)
    {
        return (Vector2)direction.ToPos();
    }

}