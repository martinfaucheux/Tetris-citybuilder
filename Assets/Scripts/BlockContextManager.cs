using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockContextManager : Singleton<BlockContextManager>
{
    public Vector3 origin;

    public BlockContext currentContext;
    void Start()
    {
        currentContext = new BlockContext();
    }

    public Vector3 GetRealWorldPosition(Vector2Int contextPosition)
    {
        float x = contextPosition.x;
        float y = contextPosition.y;
        Vector3 realWorldPos;
        realWorldPos = new Vector3(x, y, 0);
        return origin + realWorldPos;
    }

    public Vector2Int GetContextPosition(Vector3 realWorldPos)
    {
        Vector3 realPos = realWorldPos - origin;
        float x = realPos.x;
        float y = realPos.y;
        return new Vector2Int(Mathf.RoundToInt(x), Mathf.RoundToInt(y));
    }
}
