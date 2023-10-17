using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RealmManager : Singleton<RealmManager>
{
    public int unlockedRealmIndex = 0;
    public int realmSize = 10;
    public int levelRequirement = 5;
    public int levelRequirementIncr = 3;
    public ResourceGroup realmCost;
    public bool IsUnlocked(int height)
    {
        return height < (unlockedRealmIndex + 1) * realmSize;
    }

    public bool TryUnlockRealm()
    {
        ResourceGroup nextRealmCost = GetNextRealmCost();
        if (
            ResourceManager.instance.CheckCanAfford(nextRealmCost)
            && CheckHasLevelRequirement()
        )
        {
            ResourceManager.instance.Add(-nextRealmCost);
            unlockedRealmIndex++;
            TurnManager.instance.StartNewAction();
            return true;
        }
        return false;
    }

    public bool CheckIsUnlocked(int height)
    {
        bool isUnlocked = IsUnlocked(height);
        if (!isUnlocked)
            StatusTextUI.instance.SetText(
                "You need to unlock this realm before building here.",
                ColorHolder.instance.red,
                true
            );
        return isUnlocked;
    }

    public bool CheckHasLevelRequirement()
    {
        int realmLevel = GetCurrentRealmLevel();
        int realmLevelRequirement = GetNextRealmLevelRequirement();
        bool hasLevelRequirement = realmLevel >= realmLevelRequirement;
        if (!hasLevelRequirement)
        {
            StatusTextUI.instance.SetText(
                "You need to have a higher level in the previous realm to unlock this realm",
                ColorHolder.instance.red,
                true
            );
        }
        return hasLevelRequirement;
    }

    public int GetRealm(Vector2Int contextPosition) => contextPosition.y / realmSize;

    public int GetRealmLevel(int realmIndex)
    {
        int level = 0;
        GenericGrid<Block> grid = BlockContextManager.instance.currentContext.grid;
        foreach (Vector2Int v in grid)
        {
            if (GetRealm(v) == realmIndex)
            {
                HouseBlock houseBlock = grid[v] as HouseBlock;
                if (houseBlock != null)
                    level += houseBlock.GetLevel();
            }
        }
        return level;
    }

    public int GetCurrentRealmLevel() => GetRealmLevel(unlockedRealmIndex);
    public ResourceGroup GetRealmCost(int realmIndex) => realmIndex * realmCost;
    public ResourceGroup GetNextRealmCost() => GetRealmCost(unlockedRealmIndex + 1);
    public int GetRealmLevelRequirement(int realmIndex) => levelRequirement + (realmIndex - 1) * levelRequirementIncr;
    public int GetNextRealmLevelRequirement() => GetRealmLevelRequirement(unlockedRealmIndex + 1);
}
