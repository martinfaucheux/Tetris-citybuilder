using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{

    [field: SerializeField]
    [Tooltip("Resources recalculated by each block on each turn")]
    public ResourceGroup staticResources { get; private set; }

    [field: SerializeField]
    [Tooltip("Resources kept between turns")]
    public ResourceGroup permanentResources { get; private set; }

    public BlockContext currentContext => BlockContextManager.instance.currentContext;

    public bool CanAfford(ResourceGroup cost) => (staticResources - cost).isPositive();

    public void Add(ResourceGroup resources) => permanentResources += resources;

    public void CalculateCurrentResources()
    {
        staticResources = new ResourceGroup(permanentResources);
        foreach (Vector2Int contextPosition in currentContext.grid)
        {
            VirtualBlock block = currentContext.grid[contextPosition];
            staticResources += block.GetProduct();
            staticResources -= block.GetCost();
        }
    }

    public void AddPermanentProduct()
    {
        ResourceGroup resourceGroup = (
            currentContext.grid
            .Select(v => currentContext.grid[v].GetPermanentProduct())
            .Aggregate(new ResourceGroup(), (a, b) => a + b)
        );
        Add(resourceGroup);
    }
}
