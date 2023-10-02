using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NeighborPresenceBlock : Block
{
    public NeighborPresenceBlock(BlockData data) : base(data) { }

    public override ResourceGroup GetProduct()
    {
        ResourceGroup product = ((NeighborPresenceBlockData)data).baseProduct;
        if (!HasNeighbors())
            product += ((NeighborPresenceBlockData)data).bonusProduct;
        return product;
    }

    private bool HasNeighbors()
    {
        BlockContext context = BlockContextManager.instance.currentContext;
        foreach (Vector2Int offset in ((NeighborPresenceBlockData)data).emptyNeighbors)
        {
            if (context.GetBlockAtPosition(position + offset) != null)
                return true;
        }
        return false;
    }

    public override Dictionary<Vector2Int, int> GetSatisfactionAura()
    {
        int aura = ((NeighborPresenceBlockData)data).baseSatisfaction;
        if (!HasNeighbors())
            aura += ((NeighborPresenceBlockData)data).bonus;
        if (aura != 0)
            return GetDefaultAura(aura);
        return new Dictionary<Vector2Int, int>();
    }
}
