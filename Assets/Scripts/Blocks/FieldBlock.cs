using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBlock : Block
{
    public int foodAmount = 5;
    public override ResourceGroup GetProduct() => new ResourceGroup(food: foodAmount);

}
