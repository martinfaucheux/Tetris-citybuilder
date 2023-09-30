using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardForge : Singleton<CardForge>
{
    public List<BlockData> blockDataList = new List<BlockData>();
    public Vector2Int blockCountRange = new Vector2Int(3, 5);
    public AnimationCurve blockCountDistribution;
    public static int MAX_RARITY = 4;

    public Card GenerateCard() => GridToCard(GenerateGrid());

    private GenericGrid<BlockData> GenerateGrid()
    {
        GenericGrid<BlockData> grid = new GenericGrid<BlockData>();
        int blockCount = GetRandomBlockCount();
        grid[Vector2Int.zero] = GetRandomBlockData();

        for (int idx = 1; idx < blockCount; idx++)
        {
            Vector2Int position = GetAdjacentEmptyPosition(grid);
            grid[position] = GetRandomBlockData();
        }
        return grid.GetCenteredGrid();
    }

    private static Card GridToCard(GenericGrid<BlockData> grid)
    {
        Card card = ScriptableObject.CreateInstance<Card>();
        card.blockPositions = grid.Select(v => new BlockPosition(v, grid[v])).ToList();
        return card;
    }

    public Vector2Int GetAdjacentEmptyPosition(GenericGrid<BlockData> grid)
    {
        foreach (Vector2Int basePosition in grid.OrderBy(x => Random.value))
        {
            foreach (Vector2Int direction in MatrixUtils.GetNeighbors_4().OrderBy(x => Random.value))
            {
                Vector2Int position = basePosition + direction;
                if (!grid.ContainsKey(position))
                    return position;
            }
        }
        throw new System.Exception("No empty position found");
    }

    private BlockData GetRandomBlockData()
    {
        int totalWeight = blockDataList.Sum(blockData => GetWeight(blockData));
        int randomWeight = Random.Range(0, totalWeight);
        int weightSum = 0;
        foreach (BlockData blockData in blockDataList)
        {
            weightSum += GetWeight(blockData);
            if (randomWeight < weightSum)
            {
                return blockData;
            }
        }
        return blockDataList[blockDataList.Count - 1];
    }

    public static int GetWeight(BlockData blockData) => (int)Mathf.Pow(MAX_RARITY + 1 - blockData.rarity, 2);

    private int GetRandomBlockCount()
    {
        float randValue = blockCountDistribution.Evaluate(Random.value);
        int intValue = Mathf.RoundToInt(randValue * (blockCountRange.y - blockCountRange.x) + blockCountRange.x);
        return intValue;
    }

}
