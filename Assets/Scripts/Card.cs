using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using System.Runtime.InteropServices.WindowsRuntime;

[Serializable]
public class BlockPosition
{
    public Vector2Int position;
    public BlockData blockData;
    public BlockPosition(Vector2Int position, BlockData blockData)
    {
        this.position = position;
        this.blockData = blockData;
    }
}

[CreateAssetMenu(fileName = "New Card", menuName = "ScriptableObjects / Card")]
public class Card : ScriptableObject
{
    public List<BlockPosition> blockPositions = new List<BlockPosition>();

    private ResourceGroup baseCost => (
        blockPositions
        .Select(blockPosition => blockPosition.blockData.cost)
        .Aggregate(new ResourceGroup(), (a, b) => a + b)
    );

    public ResourceGroup GetCost()
    {
        ResourceGroup cost = baseCost;
        int blockCount = blockPositions.Count;
        float discount = blockCount < 4 ? 0 : 0.1f * (blockCount - 3);
        return cost - new ResourceGroup(gold: cost.gold * (int)discount);
    }
}
