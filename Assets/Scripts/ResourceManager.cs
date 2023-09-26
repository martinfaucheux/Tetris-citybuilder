using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{

    [field: SerializeField]
    public int goldAmount { get; private set; } = 100;
    public int foodAmount { get; private set; } = 0;
    public int peopleAmount { get; private set; } = 0;
    

    public event Action onGoldChange;
    public event Action onFoodChange;
    public event Action onPeopleChange;

    public void AddGold(int goldAmount)
    {
        this.goldAmount += goldAmount;
        if (onGoldChange != null)
        {
            onGoldChange();
        }
    }

    public void AddFood(int foodAmount)
    {
        this.foodAmount += foodAmount;
        if (onFoodChange != null)
        {
            onFoodChange();
        }
    }

    public void AddPeople(int peopleAmount)
    {
        this.peopleAmount += peopleAmount;
        if (onPeopleChange != null)
        {
            onPeopleChange();
        }
    }
}
