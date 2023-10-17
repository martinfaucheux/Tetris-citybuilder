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
            && !DeckManager.instance.CanAffordAny()
        )
        {
            // game over when there is no gold upkeep and no card can be played
            Debug.Log("Game over");
            StateManager.SetState(GameState.GAMEOVER);
        }
    }
}
