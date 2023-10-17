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
        return new ResourceGroup(people: peopleValue + GetLevel());
    }

    public override ResourceGroup GetPermanentProduct() => (
        new ResourceGroup(gold: -ResourceManager.instance.GetTaxAmount())
    );

    public override string GetDescription()
    {
        string description = base.GetDescription();
        int satisfaction = GetSatisfaction();
        int level = GetLevel();
        int nextLevelReq = GetSatisfactionForLevel(level + 1) - satisfaction;
        description += $"Level: <sprite name=\"Star\"> {level}\n";
        description += $"Satisfaction: <sprite name=\"Heart\"> {satisfaction}\n";
        description += $"Next level: <sprite name=\"Heart\"> {nextLevelReq}";
        return description;
    }

    public int GetLevel() => GetLevel(GetSatisfaction());

    public static int GetLevel(int satisfaction) => Mathf.FloorToInt(
        0.5f * (1 + Mathf.Sqrt(1 + 8 * satisfaction))
    );

    public static int GetSatisfactionForLevel(int level) => level * (level - 1) / 2;
}
