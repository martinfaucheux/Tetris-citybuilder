using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProximityType
{
    _8Blocks, _4Blocks
}

[CreateAssetMenu(fileName = "NeighborBonusBlockData", menuName = "ScriptableObjects / BlockData / NeighborBonus")]
public class NeighborBonusBlockData : BlockData
{
    [Tooltip("This product is yielded anyway")]
    public ResourceGroup baseProduct;
    [Tooltip("This product is yielded for each neighbor of specified types")]
    public ResourceGroup bonusProduct;
    public int baseSatisfaction;
    public int bonusSatisfaction;
    [Tooltip("blocks that will yield a bonus")]
    public List<BlockData> blockDataList;
    [Tooltip("What blocks are considered neighbors")]
    public ProximityType proximityType;
    [Tooltip("Allow to give bonus per 2 neighbors")]
    public int divider = 1;
    [Tooltip("Max neighbors counted to compute bonus")]
    [Range(1, 8)]
    public int maxStack = 8;
    public override Block CreateBlock() => new NeighborBonusBlock(this);
}