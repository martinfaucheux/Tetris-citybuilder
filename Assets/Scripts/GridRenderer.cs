using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridRenderer : MonoBehaviour
{
    public int maxY = 1000;
    public GameObject gridCellPrefab;
    CollisionMatrix matrix { get => CollisionMatrix.instance; }

    void Start()
    {
        for(int x = -matrix.maxX; x <= matrix.maxX; x++)
        {
            for(int y = 0; y < maxY; y++)
            {
                Vector3 position = matrix.GetRealWorldPosition(new Vector2Int(x, y));
                position.z = 1f;
                GameObject gridCellObj = Instantiate(gridCellPrefab, position, Quaternion.identity, transform);
                TextMeshPro text = gridCellObj.GetComponentInChildren<TextMeshPro>();
                text.text = $"{x}, {y}";
            }
        }
    }
}
