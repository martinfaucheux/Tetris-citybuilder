using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class InnBlock : Block
{
    public int goldYield = 3;
    public override ResourceGroup GetPermanentProduct() => new ResourceGroup(gold: goldYield);
}
