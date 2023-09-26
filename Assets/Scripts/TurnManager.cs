using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TurnManager : Singleton<TurnManager>
{
    [field: SerializeField]
    public int turnCount { get; private set; } = 0;

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
    }

    public void EndTurn()
    {
        StartNewTurn();
    }
}
