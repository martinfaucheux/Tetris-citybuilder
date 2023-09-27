using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Block : MonoBehaviour
{
    public ResourceGroup cost;
    public MatrixCollider matrixCollider;

    public virtual ResourceGroup GetProduct() => new ResourceGroup();
    public virtual ResourceGroup GetPermanentProduct() => new ResourceGroup();

    public void Place() => BlockManager.instance.Register(this);
}
