using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.Linq;

public class InnBlock : Block
{
    public int goldYield = 3;
    public int satisfactionAura = 1;
    public override ResourceGroup GetPermanentProduct() => new ResourceGroup(gold: goldYield);
    public override Dictionary<Vector2Int, int> GetSatisfactionAura()
    {
        return (
            MatrixUtils.GetNeighbors_8()
            .Select(v => new KeyValuePair<Vector2Int, int>(v, satisfactionAura))
            .ToDictionary(x => x.Key, x => x.Value)
        );
    }
}
