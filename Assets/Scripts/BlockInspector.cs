using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInspector : MonoBehaviour
{
    public BlockHolder blockHolder;

    private void OnMouseEnter()
    {
        BlockInspectorController.instance.FocusBlock(blockHolder);
    }

    private void OnMouseExit()
    {
        BlockInspectorController.instance.UnfocusBlock(blockHolder);
    }
}