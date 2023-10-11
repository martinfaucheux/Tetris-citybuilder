using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHolder : MonoBehaviour
{
    public Block block;
    [Tooltip("This is mostly for debugging purposes")]
    public BlockData blockData;

    private bool isPlaced => (
        BlockContextManager.instance.currentContext
        .blockRegistry.ContainsKey(block)
    );

    private void LateUpdate()
    {
        if (isPlaced)
            transform.rotation = Quaternion.identity;
    }
}
