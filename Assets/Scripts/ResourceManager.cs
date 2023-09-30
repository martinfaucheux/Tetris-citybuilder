using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ResourceManager : Singleton<ResourceManager>
{

    [field: SerializeField]
    [Tooltip("Resources recalculated by each block on each turn")]
    public ResourceGroup staticResources { get; private set; }

    [field: SerializeField]
    [Tooltip("Resources kept between turns")]
    public ResourceGroup permanentResources { get; private set; }
    public StatusTextUI statusTextUI;
    public BlockContext currentContext => BlockContextManager.instance.currentContext;

    public bool CheckCanAfford(ResourceGroup cost)
    {
        bool canAfford = (staticResources - cost).isPositive();
        if (!canAfford)
            statusTextUI.SetText(ExplainLack(cost));
        return canAfford;
    }

    public void Add(ResourceGroup resources) => permanentResources += resources;

    public void CalculateCurrentResources()
    {
        staticResources = new ResourceGroup(permanentResources);
        foreach (Vector2Int contextPosition in currentContext.grid)
        {
            Block block = currentContext.grid[contextPosition];
            staticResources += block.GetProduct();
            staticResources -= block.GetCost();
        }
    }

    public ResourceGroup GetUpkeep() => (
            currentContext.grid
            .Select(v => currentContext.grid[v].GetPermanentProduct())
            .Aggregate(new ResourceGroup(), (a, b) => a + b)
        );

    public void AddPermanentProduct() => Add(GetUpkeep());

    public string ExplainLack(ResourceGroup cost)
    {
        ResourceGroup lack = staticResources - cost;
        if (lack.gold < 0)
        {
            return $"You need don't have enough gold";
        }
        if (lack.food < 0)
        {
            return $"You need don't have enough food";
        }
        if (lack.people < 0)
        {
            return $"You need don't have enough people";
        }
        return "";
    }
}
