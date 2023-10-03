using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHolder : MonoBehaviour
{
    public Block block;
    [Tooltip("This is mostly for debugging purposes")]
    public BlockData blockData;

    private void Update()
    {
        transform.rotation = Quaternion.identity;
    }
}
