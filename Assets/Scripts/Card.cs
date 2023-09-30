using System.Collections;
using System.Collections.Generic;
using System;
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
}
