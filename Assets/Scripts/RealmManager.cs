using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealmManager : MonoBehaviour
{
    public int unlockedRealm = 0;
    public int realmSize = 10;
    public ResourceGroup realmCost;
    public bool IsUnlocked(int height)
    {
        return height < (unlockedRealm + 1) * realmSize;
    }

    public void UnlockRealm()
    {
        if (ResourceManager.instance.CheckCanAfford(realmCost))
        {
            ResourceManager.instance.Add(-realmCost);
            unlockedRealm++;
            TurnManager.instance.StartNewAction();
        }
    }
}
