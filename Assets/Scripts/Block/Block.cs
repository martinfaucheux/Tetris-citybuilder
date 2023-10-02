using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Block
{
    // TODO: Should this position should be stored in the context instead?
    public Vector2Int position { get; private set; }
    public BlockData data;
    public BlockHolder blockHolder;
    public string blockName { get => data.blockName; }

    public Block(BlockData data) { this.data = data; }

    public ResourceGroup GetCost() => data.cost;

    public void Move(Vector2Int contextPosition) => position = contextPosition;

    // TODO: should it require a context to pass to compute the product against?
    public virtual ResourceGroup GetProduct() => new ResourceGroup();
    public virtual ResourceGroup GetPermanentProduct() => new ResourceGroup();
    public virtual Dictionary<Vector2Int, int> GetSatisfactionAura() => new Dictionary<Vector2Int, int>();
    public int GetSatisfaction() => SatisfactionManager.instance.GetSatisfaction(position);

    public virtual string GetDescription()
    {
        string description = "";
        if (data.description != "") description += data.description + "\n";

        ResourceGroup product = GetProduct();
        if (!product.IsZero()) description += $"Product: {product}\n";

        ResourceGroup permanentProduct = GetPermanentProduct();
        if (!permanentProduct.IsZero()) description += $"Yield: {permanentProduct}\n";

        int satisfaction = GetSatisfaction();
        if (satisfaction != 0) description += $"Satisfaction: {satisfaction}\n";

        return description;
    }

    protected static Dictionary<Vector2Int, int> GetDefaultAura(int aura) => (
            MatrixUtils.GetNeighbors_8()
            .Select(v => new KeyValuePair<Vector2Int, int>(v, aura))
            .ToDictionary(x => x.Key, x => x.Value)
        );
}
