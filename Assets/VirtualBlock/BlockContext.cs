using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockContext
{
    public GenericGrid<VirtualBlock> grid = new GenericGrid<VirtualBlock>();
    public Dictionary<VirtualBlock, Vector2Int> blockRegistry = new Dictionary<VirtualBlock, Vector2Int>();
    public int xMax = 4;

    public BlockContext()
    {
        grid = new GenericGrid<VirtualBlock>();
        blockRegistry = new Dictionary<VirtualBlock, Vector2Int>();
    }

    public void Register(VirtualBlock block)
    {
        grid[block.position] = block;
        blockRegistry[block] = block.position;
    }

    public bool IsValidPosition(Vector2Int position) => position.y >= 0 && Mathf.Abs(position.x) <= xMax;

    public VirtualBlock GetBlockAtPosition(Vector2Int position)
    {
        if (grid.ContainsKey(position))
            return grid[position];
        return null;
    }
}
