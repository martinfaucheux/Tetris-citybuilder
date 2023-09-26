using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TurnManager : Singleton<TurnManager>
{
    int turnCount = 0;

    public UnityEvent onTurnStart;


    // Start is called before the first frame update
    void Start()
    {
        StartNewTurn();
    }

    public void StartNewTurn()
    {
        turnCount++;
        onTurnStart?.Invoke();

        // foreach block, compute cost and product
        // update UI
    }

    public void EndTurn()
    {
        StartNewTurn();
    }
}
