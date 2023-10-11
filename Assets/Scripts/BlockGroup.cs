using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;



public class BlockGroup
{
    public GenericGrid<Block> grid;
    public ResourceGroup cost { get => grid.Select(v => grid[v].GetCost()).Aggregate((x, y) => x + y); }
    public Transform transform;

    public BlockGroup(Card card)
    {
        grid = new GenericGrid<Block>();
        foreach (BlockPosition blockPosition in card.blockPositions)
        {
            Block block = blockPosition.blockData.CreateBlock();
            grid[blockPosition.position] = block;
        }
    }

    public void Move(Vector2Int contextPosition)
    {
        foreach (Vector2Int relativePosition in grid)
        {
            Block block = grid[relativePosition];
            block.Move(contextPosition + relativePosition);
        }
        if (transform != null)
            transform.position = BlockContextManager.instance.GetRealWorldPosition(contextPosition);
        else Debug.LogWarning("No transform found when moving the block group");
    }

    public void Rotate(Vector2Int contextPosition, int rotation)
    {
        GenericGrid<Block> newGrid = new GenericGrid<Block>();
        foreach (Vector2Int relativePosition in grid)
        {
            Block block = grid[relativePosition];
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
            Block childBlock = grid[relativePosition];
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
        blockGameObject.name = "BlockGroup";

        foreach (Vector2Int relativePosition in grid)
        {
            Block block = grid[relativePosition];
            block.Move(contextPosition + relativePosition);
            Vector3 realPosition = BlockContextManager.instance.GetRealWorldPosition(contextPosition + relativePosition);

            GameObject newBlockObj = GameObject.Instantiate(block.data.prefab, realPosition, Quaternion.identity, blockGameObject.transform);
            newBlockObj.name = $"{block.data.blockName} (Block)";
            BlockHolder blockHolder = newBlockObj.GetComponent<BlockHolder>();
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
            Block block = grid[relativePosition];
            BlockContextManager.instance.currentContext.Register(block);
        }
    }

    public bool IsValidPosition(BlockContext context, Vector2Int contextPosition) => (
        grid.Select(v => contextPosition + v).All(v => context.IsValidPosition(v))
    );

    // TODO: function to get the 2d bounds of the group
    public Bounds GetBounds() => grid.GetBounds();
}
