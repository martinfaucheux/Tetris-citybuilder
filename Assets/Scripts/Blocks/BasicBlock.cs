using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasicBlock : Block
{
    public ResourceGroup product = new ResourceGroup();
    public ResourceGroup permanentProduct = new ResourceGroup();
    public int satisfactionAura = 0;

    public override ResourceGroup GetProduct() => product;
    public override ResourceGroup GetPermanentProduct() => permanentProduct;
    public override Dictionary<Vector2Int, int> GetSatisfactionAura()
    {
        return (
            MatrixUtils.GetNeighbors_8()
            .Select(v => new KeyValuePair<Vector2Int, int>(v, satisfactionAura))
            .ToDictionary(x => x.Key, x => x.Value)
        );
    }

}