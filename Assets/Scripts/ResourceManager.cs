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
    public StatusTextUI statusTextUI;
    public int taxCycleDuration = 10;
    public int baseTax = 0;
    [Range(1, 10)]
    public int taxIncrease = 1;
    public BlockContext currentContext => BlockContextManager.instance.currentContext;

    void Start()
    {
        // enforce calculation even before the first turn starts
        CalculateCurrentResources();
    }

    public bool CheckCanAfford(ResourceGroup cost)
    {
        bool canAfford = CanAfford(cost);
        if (!canAfford)
            statusTextUI.SetText(ExplainLack(cost), ColorHolder.instance.red, true);
        return canAfford;
    }

    public bool CanAfford(ResourceGroup cost) => (staticResources - cost).isPositive();

    public void Add(ResourceGroup resources) => permanentResources += resources;

    public void CalculateCurrentResources()
    {
        staticResources = new ResourceGroup(permanentResources);
        foreach (Vector2Int contextPosition in currentContext.grid)
        {
            Block block = currentContext.grid[contextPosition];
            staticResources += block.GetProduct();
        }
        foreach (Card card in TurnManager.instance.playedCards)
            // cost is computed from cards to take the discount into account
            staticResources -= card.GetCost();
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

    public int GetTaxAmount() => baseTax + taxIncrease * (TurnManager.instance.turnCount / taxCycleDuration);
}
