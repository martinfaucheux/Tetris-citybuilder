using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldVirtualBlock : VirtualBlock
{
    public FieldVirtualBlock(BlockData data) : base(data) { }
    public override ResourceGroup GetProduct()
    {
        int foodValue = ((FieldBlockData)data).baseFoodValue;
        VirtualBlock otherblock = (
            BlockContextManager.instance.currentContext
            .GetBlockAtPosition(position + Vector2Int.up
        ));
        if (otherblock == null)
            foodValue += ((FieldBlockData)data).skyFoodBonus;
        return new ResourceGroup(food: foodValue);
    }
}
