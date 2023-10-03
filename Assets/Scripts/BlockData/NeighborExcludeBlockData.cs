using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NeighborExcludeBlockData", menuName = "ScriptableObjects / BlockData / NeighborExclude")]
public class NeighborExcludeBlockData : BlockData
{
    public ResourceGroup baseProduct;
    public ResourceGroup bonusProduct;
    public int baseSatisfaction;
    public int bonus;
    public List<Vector2Int> emptyNeighbors;
    public override Block CreateBlock() => new NeighborExcludeBlock(this);
}
