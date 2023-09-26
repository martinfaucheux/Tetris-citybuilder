using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Price 
{
    int gold;
    int food;
    int people;

    public Price(int gold = 0, int food = 0, int people = 0)
    {
        this.gold = gold;
        this.food = food;
        this.people = people;
    }

    public static Price operator +(Price price1, Price price2)
    {
        return new Price(
            price1.gold + price2.gold,
            price1.food + price2.food,
            price1.people + price2.people
        );
    }
}
