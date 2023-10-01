using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

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

    public ResourceGroup cost => blockPositions
        .Select(blockPosition => blockPosition.blockData.cost)
        .Aggregate(new ResourceGroup(), (a, b) => a + b);
}
