using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : Singleton<BlockManager>
{
    [field: SerializeField]
    public List<Block> blockList { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        blockList = new List<Block>();
    }

    public void Register(Block block) => blockList.Add(block);
    public void Unregister(Block block) => blockList.Remove(block);
    public void AddPermanentProduct()
    {
        foreach (Block block in blockList)
        {
            ResourceManager.instance.Add(block.GetPermanentProduct());
        }
    }
}
