using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultBlockData", menuName = "ScriptableObjects / BlockData / Default")]
public class BlockData : ScriptableObject
{
    public string blockName;
    public string description;
    public ResourceGroup cost;
    public GameObject prefab;

    public virtual VirtualBlock CreateBlock() => new DefaultVirtualBlock(this);
}