using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FieldBlockData", menuName = "ScriptableObjects / BlockData / Field")]
public class FieldBlockData : BlockData
{
    public int baseFoodValue = 3;
    public int skyFoodBonus = 5;
    public override Block CreateBlock() => new FieldBlock(this);
}
