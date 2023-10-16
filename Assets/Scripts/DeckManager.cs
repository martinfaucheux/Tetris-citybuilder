using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckManager : Singleton<DeckManager>
{
    [Range(0, 10)]
    public int handCardCount = 3;
    public List<Card> drawCards = new List<Card>();
    public List<Card> handCards = new List<Card>();
    public List<Card> discardCards = new List<Card>();
    public event Action onDeckChanged;

    public List<Card> allCards => drawCards.Concat(handCards).Concat(discardCards).ToList();

    public void RefreshHand()
    {
        DiscardHand();
        DrawHand();
    }

    public void DrawHand()
    {
        for (int i = 0; i < handCardCount; i++)
            Draw();
    }

    public void Draw()
    {
        if (drawCards.Count == 0)
            ShuffleDiscardIntoDraw();

        Card card = drawCards[0];
        drawCards.RemoveAt(0);
        handCards.Add(card);
    }

    public void Discard(Card card)
    {
        handCards.Remove(card);
        discardCards.Add(card);
    }

    public void DiscardHand()
    {
        discardCards.AddRange(handCards);
        handCards.Clear();
        onDeckChanged?.Invoke();
    }

    public void ShuffleDiscardIntoDraw()
    {
        drawCards.AddRange(discardCards);
        discardCards.Clear();
        ShuffleDraw();
    }

    public void ShuffleDraw()
    {
        drawCards = drawCards.OrderBy(x => UnityEngine.Random.value).ToList();
    }

    public void Reset()
    {
        drawCards.AddRange(discardCards);
        drawCards.AddRange(handCards);
        ShuffleDraw();
        discardCards.Clear();
        handCards.Clear();
    }

    public void AddCard(Card card)
    {
        drawCards.Add(card);
        onDeckChanged?.Invoke();
    }

    public bool CanAffordAny() => drawCards.Any(card => ResourceManager.instance.CanAfford(card.cost));

    public Card GetFirstHandCard() => handCards.Any() ? handCards[0] : null;
}
