using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BlockData : ScriptableObject
{
    public string blockName;
    public string description;
    public ResourceGroup cost;
    public GameObject prefab;

    public abstract VirtualBlock CreateBlock();
}