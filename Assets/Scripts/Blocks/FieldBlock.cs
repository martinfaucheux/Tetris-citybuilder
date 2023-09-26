using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBlock : Block
{
    int foodAmount = 5;
    public override void OnPlace()
    {
        ResourceManager.instance.AddFood(foodAmount);
    }
}
