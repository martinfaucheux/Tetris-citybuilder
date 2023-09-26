using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{

    [field: SerializeField]
    public ResourceGroup resources { get; private set; }

    [field: SerializeField]
    public ResourceGroup initialResources { get; private set; }

    private void Start()
    {
        Calculate();
    }

    public bool CanAfford(ResourceGroup cost) => (resources - cost).isPositive();

    public void Add(ResourceGroup resources) => this.resources += resources;

    public void Calculate()
    {
        resources = new ResourceGroup(initialResources);
        foreach (Block block in BlockManager.instance.blockList)
        {
            resources += block.GetProduct();
            resources -= block.cost;
        }
    }
}
