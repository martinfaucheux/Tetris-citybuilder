using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldBlock : Block
{
    public int foodAmount = 3;
    public int skyBonus = 5;
    public override ResourceGroup GetProduct()
    {
        int foodAmount = this.foodAmount;
        if (!matrixCollider.GetObjectsInDirection(Direction.UP).Any())
            foodAmount += skyBonus;

        return new ResourceGroup(food: foodAmount);
    }

}
