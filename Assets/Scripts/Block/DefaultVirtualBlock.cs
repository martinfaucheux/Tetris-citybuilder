using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DefaultBlock : Block
{
    public DefaultBlock(BlockData data) : base(data) { }

    public override ResourceGroup GetProduct() => ((DefaultBlockData)data).product;
    public override ResourceGroup GetPermanentProduct() => ((DefaultBlockData)data).permanentProduct;
    public override Dictionary<Vector2Int, int> GetSatisfactionAura()
    {
        return (
            MatrixUtils.GetNeighbors_8()
            .Select(v => new KeyValuePair<Vector2Int, int>(v, ((DefaultBlockData)data).satisfactionAura))
            .ToDictionary(x => x.Key, x => x.Value)
        );
    }
}
