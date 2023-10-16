using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealmManager : Singleton<RealmManager>
{
    public int unlockedRealm = 0;
    public int realmSize = 10;
    public ResourceGroup realmCost;
    public bool IsUnlocked(int height)
    {
        return height < (unlockedRealm + 1) * realmSize;
    }

    public bool TryUnlockRealm()
    {
        if (ResourceManager.instance.CheckCanAfford(realmCost))
        {
            ResourceManager.instance.Add(-realmCost);
            unlockedRealm++;
            TurnManager.instance.StartNewAction();
            return true;
        }
        return false;
    }

    public bool CheckIsUnlocked(int height)
    {
        bool isUnlocked = IsUnlocked(height);
        if (!isUnlocked)
            StatusTextUI.instance.SetText("You need to unlock this realm before building here.", ColorHolder.instance.red, true);
        return isUnlocked;
    }
}
