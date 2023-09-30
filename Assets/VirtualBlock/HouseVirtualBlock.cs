using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseVirtualBlock : VirtualBlock
{
    public HouseVirtualBlock(BlockData data) : base(data) { }

    // TODO: add bonus for satisfaction
    public override ResourceGroup GetProduct() => new ResourceGroup(people: ((HouseBlockData)data).peopleAmount);

    public override ResourceGroup GetPermanentProduct() => new ResourceGroup(gold: -((HouseBlockData)data).tax);
}
