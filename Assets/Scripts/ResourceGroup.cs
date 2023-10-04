using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct ResourceGroup
{
    public int gold;
    public int food;
    public int people;

    public ResourceGroup(int gold = 0, int food = 0, int people = 0)
    {
        this.gold = gold;
        this.food = food;
        this.people = people;
    }

    public ResourceGroup(ResourceGroup other)
    {
        this.gold = other.gold;
        this.food = other.food;
        this.people = other.people;
    }

    public static ResourceGroup operator +(ResourceGroup price1, ResourceGroup price2)
    {
        return new ResourceGroup(
            price1.gold + price2.gold,
            price1.food + price2.food,
            price1.people + price2.people
        );
    }

    public static ResourceGroup operator -(ResourceGroup price1, ResourceGroup price2)
    {
        return price1 + (-1) * price2;
    }

    public static ResourceGroup operator -(ResourceGroup price1)
    {
        return (-1) * price1;
    }

    public static ResourceGroup operator *(int mult, ResourceGroup cost)
    {
        return new ResourceGroup(cost.gold * mult, cost.food * mult, cost.people * mult);
    }

    public bool isPositive() => gold >= 0 && food >= 0 && people >= 0;

    public override string ToString()
    {
        string result = "";
        if (gold != 0) result += $"Gold: {gold}, ";
        if (food != 0) result += $"Food: {food}, ";
        if (people != 0) result += $"People: {people}, ";
        return result.TrimEnd(',', ' ');
    }

    public string ToStringIcon()
    {
        string result = "";
        if (gold != 0) result += $"<sprite name=\"Gold\"> {gold} ";
        if (food != 0) result += $"<sprite name=\"Food\"> {food} ";
        if (people != 0) result += $"<sprite name=\"People\"> {people} ";
        return result.TrimEnd(' ');
    }

    public bool IsZero() => gold == 0 && food == 0 && people == 0;
}
