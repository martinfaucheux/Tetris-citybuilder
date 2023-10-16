using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameOverChecker : MonoBehaviour
{
    public void Check()
    {
        if (
            ResourceManager.instance.GetUpkeep().gold <= 0
            && !CanAffordAnyCard()
        )
            // game over when there is no gold upkeep and no card can be played
            StateManager.SetState(GameState.GAMEOVER);
    }

    private bool CanAffordAnyCard() => DeckManager.instance.allCards.Any(
        card => ResourceManager.instance.CanAfford(card.cost)
    );
}
