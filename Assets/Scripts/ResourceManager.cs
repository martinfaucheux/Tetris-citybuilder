using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    public event Action onGoldChange;

    [field: SerializeField]
    public int goldAmount { get; private set; } = 100;

    public void AddGold(int goldAmount)
    {
        this.goldAmount += goldAmount;
        if (onGoldChange != null)
        {
            onGoldChange();
        }
    }
}
