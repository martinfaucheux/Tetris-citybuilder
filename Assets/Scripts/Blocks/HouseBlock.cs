using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBlock : Block
{
    public int peopleAmount = 3;
    public override void OnPlace()
    {
        ResourceManager.instance.AddPeople(peopleAmount);
    }
}
