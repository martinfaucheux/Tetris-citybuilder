using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultBlockData", menuName = "ScriptableObjects / BlockData / Default")]
public class DefaultBlockData : BlockData
{
    public ResourceGroup product;
    public ResourceGroup permanentProduct;
    public int satisfactionAura = 0;

    public override Block CreateBlock() => new DefaultBlock(this);
}
