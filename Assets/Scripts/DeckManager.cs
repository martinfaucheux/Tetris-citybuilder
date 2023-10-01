using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckManager : Singleton<DeckManager>
{
    public List<Card> cardList = new List<Card>();

    public void AddCard(Card card) => cardList.Add(card);

    public bool CanAffordAny() => cardList.Any(card => ResourceManager.instance.CanAfford(card.cost));
}
