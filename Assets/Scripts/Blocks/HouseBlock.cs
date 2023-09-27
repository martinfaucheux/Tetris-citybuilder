using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBlock : Block
{
    public int peopleAmount = 3;
    public int taxPerPeople = 1;
    public override ResourceGroup GetProduct() => new ResourceGroup(people: peopleAmount);

    public override ResourceGroup GetPermanentProduct() => new ResourceGroup(gold: -peopleAmount * taxPerPeople);
}
