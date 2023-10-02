using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckManager : Singleton<DeckManager>
{
    [Range(0, 10)]
    public int initCardCount = 3;
    public List<Card> cardList = new List<Card>();
    public event Action onDeckChanged;

    void Start()
    {
        for (int i = 0; i < initCardCount; i++)
            AddCard(CardForge.instance.GenerateCard());
    }

    public void AddCard(Card card)
    {
        cardList.Add(card);
        onDeckChanged?.Invoke();
    }

    public bool CanAffordAny() => cardList.Any(card => ResourceManager.instance.CanAfford(card.cost));
}
