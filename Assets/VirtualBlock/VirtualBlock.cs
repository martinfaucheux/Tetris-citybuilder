using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VirtualBlock
{
    // TODO: Should this position should be stored in the context instead?
    public Vector2Int position { get; private set; }
    public BlockData data;
    public BlockHolder blockHolder;
    public VirtualBlock(BlockData data) { this.data = data; }

    public ResourceGroup GetCost() => data.cost;

    public void Move(Vector2Int contextPosition) => position = contextPosition;

    public virtual ResourceGroup GetProduct() => new ResourceGroup();
    public virtual ResourceGroup GetPermanentProduct() => new ResourceGroup();
}
