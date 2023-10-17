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
    public List<Card> playedCards = new List<Card>();

    void Start()
    {
        StateManager.instance.onStateChange += OnStateChange;
    }

    void OnDestroy()
    {
        StateManager.instance.onStateChange -= OnStateChange;
    }

    private void OnStateChange(GameState previous, GameState current)
    {
        // first turn should start after the first draft phase
        if (
            previous == GameState.DRAFT
            && current == GameState.TURN
            && turnCount == 0
        )
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
        if (StateManager.currentState != GameState.GAMEOVER)
            StartNewAction();
    }

    public void EndTurn()
    {
        Debug.Log("Ending turn");
        StartNewTurn();
    }
}
