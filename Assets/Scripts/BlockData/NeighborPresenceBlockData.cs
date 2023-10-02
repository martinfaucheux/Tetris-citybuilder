using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NeighborPresenceBlockData", menuName = "ScriptableObjects / BlockData / NeighborPresence")]
public class NeighborPresenceBlockData : BlockData
{
    public ResourceGroup baseProduct;
    public ResourceGroup bonusProduct;
    public int baseSatisfaction;
    public int bonus;
    public List<Vector2Int> emptyNeighbors;
    public override Block CreateBlock() => new DefaultBlock(this);
}
