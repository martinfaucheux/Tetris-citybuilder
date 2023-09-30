using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBlock : Block
{
    public FieldBlock(BlockData data) : base(data) { }
    public override ResourceGroup GetProduct()
    {
        int foodValue = ((FieldBlockData)data).baseFoodValue;
        Block otherblock = (
            BlockContextManager.instance.currentContext
            .GetBlockAtPosition(position + Vector2Int.up
        ));
        if (otherblock == null)
            foodValue += ((FieldBlockData)data).skyFoodBonus;
        return new ResourceGroup(food: foodValue);
    }
}
