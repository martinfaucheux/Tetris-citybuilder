using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBlock : Block
{
    public HouseBlock(BlockData data) : base(data) { }

    // TODO: add bonus for satisfaction
    public override ResourceGroup GetProduct()
    {
        int peopleValue = ((HouseBlockData)data).peopleAmount;
        int satisfaction = SatisfactionManager.instance.GetSatisfaction(position);
        return new ResourceGroup(people: peopleValue + satisfaction);
    }

    public override ResourceGroup GetPermanentProduct() => (
        new ResourceGroup(gold: -ResourceManager.instance.GetTaxAmount())
    );

    public override string GetDescription()
    {
        string description = base.GetDescription();
        int satisfaction = GetSatisfaction();
        if (satisfaction != 0) description += $"Satisfaction: {satisfaction}\n";
        return description;
    }
}
