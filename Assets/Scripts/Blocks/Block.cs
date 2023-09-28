using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Block : MonoBehaviour
{
    public string blockName;
    [TextArea()]
    public string description;
    public ResourceGroup cost;
    public MatrixCollider matrixCollider;
    [field: SerializeField]
    public bool isPlaced { get; private set; } = false;

    public virtual ResourceGroup GetProduct() => new ResourceGroup();
    public virtual ResourceGroup GetPermanentProduct() => new ResourceGroup();

    public void Place()
    {
        matrixCollider.SynchronizePosition();
        isPlaced = true;
        BlockManager.instance.Register(this);
    }

    public virtual Dictionary<Vector2Int, int> GetSatisfactionAura()
    {
        return new Dictionary<Vector2Int, int>();
    }

    public int GetSatisfaction() => SatisfactionManager.instance.GetSatisfaction(matrixCollider.matrixPosition);

    public virtual string GetDescription()
    {
        string description = "";
        if (this.description != "") description += this.description + "\n";

        ResourceGroup product = GetProduct();
        if (!product.IsZero()) description += $"Product: {product}\n";

        ResourceGroup permanentProduct = GetPermanentProduct();
        if (!permanentProduct.IsZero()) description += $"Yield: {permanentProduct}\n";

        int satisfaction = GetSatisfaction();
        if (satisfaction != 0) description += $"Satisfaction: {satisfaction}\n";

        return description;
    }
}