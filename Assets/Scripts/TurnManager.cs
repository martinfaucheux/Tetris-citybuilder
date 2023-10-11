using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TurnManager : Singleton<TurnManager>
{
    [field: SerializeField]
    public int turnCount { get; private set; } = 0;

    public UnityEvent onTurnStart;
    public UnityEvent onNewPlayerAction;


    // Start is called before the first frame update
    void Start()
    {
        StartNewTurn();
    }

    public void StartNewAction()
    {
        onNewPlayerAction?.Invoke();
    }

    public void StartNewTurn()
    {
        turnCount++;
        onTurnStart?.Invoke();
        StartNewAction();
    }

    public void EndTurn()
    {
        Debug.Log("Ending turn");
        StartNewTurn();
    }
}
