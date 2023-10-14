using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockContext
{
    public GenericGrid<Block> grid = new GenericGrid<Block>();
    public Dictionary<Block, Vector2Int> blockRegistry = new Dictionary<Block, Vector2Int>();
    public int xMax = 4;

    public BlockContext()
    {
        grid = new GenericGrid<Block>();
        blockRegistry = new Dictionary<Block, Vector2Int>();
    }

    public void Register(Block block)
    {
        grid[block.position] = block;
        blockRegistry[block] = block.position;
    }

    public bool IsValidPosition(Vector2Int position) => position.y >= 0 && Mathf.Abs(position.x) <= xMax;

    public Block GetBlockAtPosition(Vector2Int position)
    {
        if (grid.ContainsKey(position))
            return grid[position];
        return null;
    }

    public int GetMaxHeight() => grid.Any() ? grid.Select(v => v.y).Max() + 1 : 0;
}
