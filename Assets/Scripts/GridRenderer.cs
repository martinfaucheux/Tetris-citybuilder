using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridRenderer : MonoBehaviour
{
    public int maxY = 1000;
    public GameObject gridCellPrefab;
    BlockContextManager contextManager { get => BlockContextManager.instance; }

    void Start()
    {
        for (int x = -contextManager.currentContext.xMax; x <= contextManager.currentContext.xMax; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
                Vector3 position = contextManager.GetRealWorldPosition(new Vector2Int(x, y));
                position.z = 1f;
                GameObject gridCellObj = Instantiate(gridCellPrefab, position, Quaternion.identity, transform);
                TextMeshPro text = gridCellObj.GetComponentInChildren<TextMeshPro>();
                text.text = $"{x}, {y}";
            }
        }
    }
}
