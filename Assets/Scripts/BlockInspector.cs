using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInspector : MonoBehaviour
{
    public Block block;

    private void OnMouseEnter()
    {
        if (block.isPlaced)
            BlockInspectorController.instance.FocusBlock(block);
    }

    private void OnMouseExit()
    {
        if (block.isPlaced)
            BlockInspectorController.instance.UnfocusBlock(block);
    }
}