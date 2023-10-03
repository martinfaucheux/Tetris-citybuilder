using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NeighborBonusBlock : Block
{
    public NeighborBonusBlock(BlockData data) : base(data) { }

    public override ResourceGroup GetProduct() => (
        ((NeighborBonusBlockData)data).baseProduct + GetBonusProduct()
    );

    public override Dictionary<Vector2Int, int> GetSatisfactionAura() => (
        GetDefaultAura(((NeighborBonusBlockData)data).baseSatisfaction + GetBonusAuraValue())
    );

    private ResourceGroup GetBonusProduct() => (
        GetNeighborCount() * ((NeighborBonusBlockData)data).bonusProduct
    );

    private int GetBonusAuraValue() => (
        GetNeighborCount() * ((NeighborBonusBlockData)data).bonusSatisfaction
    );

    private int GetNeighborCount()
    {
        NeighborBonusBlockData _data = (NeighborBonusBlockData)data;
        List<string> validBlockNames = _data.blockDataList.Select(d => d.blockName).ToList();

        int count = 0;
        BlockContext context = BlockContextManager.instance.currentContext;
        foreach (Vector2Int offset in GetNeighborPositions())
        {
            Block block = context.GetBlockAtPosition(position + offset);
            if (block != null && validBlockNames.Contains(block.data.blockName))
                count++;
        }
        return Mathf.Min(count / _data.divider, _data.maxStack);
    }

    public override string GetDescription()
    {
        NeighborBonusBlockData data = (NeighborBonusBlockData)this.data;
        string description = base.GetDescription();

        if (!data.bonusProduct.IsZero())
        {
            ResourceGroup computedBonusProduct = GetBonusProduct();
            description += computedBonusProduct.IsZero() ? "No product bonus" : $"Product bonus: {computedBonusProduct}\n";
        }

        if (data.bonusSatisfaction != 0)
        {
            int computedBonusAuraValue = GetBonusAuraValue();
            description += computedBonusAuraValue == 0 ? "No satisfaction bonus" : $"Satisfaction bonus: {computedBonusAuraValue}\n";
        }
        return description;
    }

    private List<Vector2Int> GetNeighborPositions()
    {
        switch (((NeighborBonusBlockData)data).proximityType)
        {
            case ProximityType._8Blocks:
                return MatrixUtils.GetNeighbors_8();
            case ProximityType._4Blocks:
                return MatrixUtils.GetNeighbors_4();
            default:
                throw new System.Exception("Invalid proximity type");
        }
    }
}
