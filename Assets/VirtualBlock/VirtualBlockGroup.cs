using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;



public class VirtualBlockGroup
{
    public GenericGrid<VirtualBlock> grid;
    public ResourceGroup cost { get => grid.Select(v => grid[v].GetCost()).Aggregate((x, y) => x + y); }
    public Transform transform;

    public VirtualBlockGroup(Card card)
    {
        grid = new GenericGrid<VirtualBlock>();
        foreach (BlockPosition blockPosition in card.blockPositions)
        {
            VirtualBlock block = blockPosition.blockData.CreateBlock();
            grid[blockPosition.position] = block;
        }
    }

    public void Move(Vector2Int contextPosition)
    {
        foreach (Vector2Int relativePosition in grid)
        {
            VirtualBlock block = grid[relativePosition];
            block.Move(contextPosition + relativePosition);
        }
        if (transform != null)
            transform.position = BlockContextManager.instance.GetRealWorldPosition(contextPosition);
        else Debug.LogWarning("No transform found when moving the block group");
    }

    public Vector2Int GetLowestPosition(BlockContext context, int xBase)
    {
        int yBaseMin = 0;
        foreach (Vector2Int relativePosition in grid)
        {
            VirtualBlock childBlock = grid[relativePosition];
            int x = xBase + relativePosition.x;

            int yMin = 0;
            foreach (Vector2Int contextPosition in context.grid.Where(v => v.x == x))
            {
                yMin = Mathf.Max(contextPosition.y + 1, yMin);
            }
            yBaseMin = Mathf.Max(yBaseMin, yMin - relativePosition.y);
        }
        return new Vector2Int(xBase, yBaseMin);
    }

    public GameObject InstantiateGameObject(Vector2Int contextPosition)
    {
        GameObject blockGameObject = new GameObject();
        blockGameObject.transform.position = BlockContextManager.instance.GetRealWorldPosition(contextPosition);
        transform = blockGameObject.transform;

        foreach (Vector2Int relativePosition in grid)
        {
            VirtualBlock block = grid[relativePosition];
            Vector3 realPosition = BlockContextManager.instance.GetRealWorldPosition(contextPosition + relativePosition);

            GameObject newObj = GameObject.Instantiate(block.data.prefab, realPosition, Quaternion.identity, blockGameObject.transform);
            block.blockHolder = newObj.GetComponent<BlockHolder>();
        }
        return blockGameObject;
    }

    public void Register()
    {
        foreach (Vector2Int relativePosition in grid)
        {
            VirtualBlock block = grid[relativePosition];
            BlockContextManager.instance.currentContext.Register(block);
        }
    }

    public bool IsValidPosition(BlockContext context, Vector2Int contextPosition) => (
        grid.Select(v => contextPosition + v).All(v => context.IsValidPosition(v))
    );
}
