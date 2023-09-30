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

    public void Rotate(Vector2Int contextPosition, int rotation)
    {
        GenericGrid<VirtualBlock> newGrid = new GenericGrid<VirtualBlock>();
        foreach (Vector2Int relativePosition in grid)
        {
            VirtualBlock block = grid[relativePosition];
            Vector2Int newRelativePosition = VectorUtils.Rotate(relativePosition, rotation);
            newGrid[newRelativePosition] = block;
            block.Move(contextPosition + newRelativePosition);
        }
        // TODO: this should also handle transform rotation
        grid = newGrid;
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
            block.Move(contextPosition + relativePosition);
            Vector3 realPosition = BlockContextManager.instance.GetRealWorldPosition(contextPosition + relativePosition);

            GameObject newObj = GameObject.Instantiate(block.data.prefab, realPosition, Quaternion.identity, blockGameObject.transform);
            BlockHolder blockHolder = newObj.GetComponent<BlockHolder>();
            block.blockHolder = blockHolder;
            blockHolder.block = block;
            blockHolder.blockData = block.data;

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

    // TODO: function to get the 2d bounds of the group
    public BoundsInt GetBounds()
    {
        Vector2Int min = new Vector2Int(int.MaxValue, int.MaxValue);
        Vector2Int max = new Vector2Int(int.MinValue, int.MinValue);
        foreach (Vector2Int relativePosition in grid)
        {
            min = Vector2Int.Min(min, relativePosition);
            max = Vector2Int.Max(max, relativePosition);
        }
        return new BoundsInt((Vector3Int)min, (Vector3Int)(max - min + Vector2Int.one));
    }
}
