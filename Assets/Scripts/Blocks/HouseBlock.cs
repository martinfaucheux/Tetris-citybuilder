using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBlock : Block
{
    public int peopleAmount = 3;
    public int taxPerPeople = 1;

    public override ResourceGroup GetProduct()
    {
        int peopleYield = peopleAmount + GetSatisfaction();
        return new ResourceGroup(people: peopleYield);
    }

    public override ResourceGroup GetPermanentProduct() => new ResourceGroup(gold: -peopleAmount * taxPerPeople);
}
