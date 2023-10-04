using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NeighborExcludeBlock : Block
{
    public NeighborExcludeBlock(BlockData data) : base(data) { }

    public override ResourceGroup GetProduct() => (
        ((NeighborExcludeBlockData)data).baseProduct + GetBonusProduct()
    );

    private ResourceGroup GetBonusProduct() => (
        HasNeighbors() ? new ResourceGroup() : ((NeighborExcludeBlockData)data).bonusProduct
    );

    private int GetBonusAuraValue() => (
        HasNeighbors() ? 0 : ((NeighborExcludeBlockData)data).bonus
    );

    private bool HasNeighbors()
    {
        BlockContext context = BlockContextManager.instance.currentContext;
        foreach (Vector2Int offset in ((NeighborExcludeBlockData)data).emptyNeighbors)
        {
            if (context.GetBlockAtPosition(position + offset) != null)
                return true;
        }
        return false;
    }

    public override Dictionary<Vector2Int, int> GetSatisfactionAura()
    {
        int aura = ((NeighborExcludeBlockData)data).baseSatisfaction + GetBonusAuraValue();
        if (aura != 0)
            return GetDefaultAura(aura);
        return new Dictionary<Vector2Int, int>();
    }


    public override string GetDescription()
    {
        NeighborExcludeBlockData data = (NeighborExcludeBlockData)this.data;
        string description = base.GetDescription();

        if (!data.bonusProduct.IsZero())
        {
            ResourceGroup computedBonusProduct = GetBonusProduct();
            description += computedBonusProduct.IsZero() ? "No product bonus" : $"Product bonus: {computedBonusProduct.ToStringIcon()}\n";
        }

        if (data.bonus != 0)
        {
            int computedBonusAuraValue = GetBonusAuraValue();
            description += computedBonusAuraValue == 0 ? "No satisfaction bonus" : $"Satisfaction bonus: {computedBonusAuraValue}\n";
        }
        return description;
    }
}
