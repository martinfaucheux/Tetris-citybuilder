using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HouseBlockData", menuName = "ScriptableObjects / BlockData / House")]
public class HouseBlockData : BlockData
{
    public int peopleAmount = 3;
    public int tax = 3;
    public override VirtualBlock CreateBlock() => new HouseVirtualBlock(this);
}
