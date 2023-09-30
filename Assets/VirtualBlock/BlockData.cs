using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BlockData", menuName = "ScriptableObjects / BlockData")]
public class BlockData : ScriptableObject
{
    public string blockName;
    public string description;
    public ResourceGroup cost;
    public GameObject prefab;

    public BlockBinding.BlockType blockType;

    public VirtualBlock CreateBlock()
    {
        VirtualBlock block = (VirtualBlock)Activator.CreateInstance(BlockBinding.GetClass(blockType), this);
        block.data = this;
        return block;
    }
}

public static class BlockBinding
{
    public enum BlockType
    {
        Default,
    }

    public static Type GetClass(BlockType blockType)
    {
        switch (blockType)
        {
            case BlockType.Default:
                return typeof(DefaultVirtualBlock);
            default:
                throw new Exception("Invalid block type");
        }
    }
}