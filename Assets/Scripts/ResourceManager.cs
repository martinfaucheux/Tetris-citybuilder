using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{

    [field: SerializeField]
    [Tooltip("Resources recalculated by each block on each turn")]
    public ResourceGroup staticResources { get; private set; }

    [field: SerializeField]
    [Tooltip("Resources kept between turns")]
    public ResourceGroup permanentResources { get; private set; }

    public bool CanAfford(ResourceGroup cost) => (staticResources - cost).isPositive();

    public void Add(ResourceGroup resources) => permanentResources += resources;

    public void CalculateCurrentResources()
    {
        staticResources = new ResourceGroup(permanentResources);
        foreach (Block block in BlockManager.instance.blockList)
        {
            staticResources += block.GetProduct();
            staticResources -= block.cost;
        }
    }
}
