using System;
using System.Collections;
using UnityEngine;

public enum GameState { TURN, DRAFT, GAMEOVER }

public class StateManager : Singleton<StateManager>
{
    public static GameState currentState { get; private set; }

    [field: SerializeField]
    // this is only to show in the inpsector, it is not used
    private GameState _currentState;

    [field: SerializeField]
    private GameState _defaultState = GameState.TURN;
    public event Action<GameState, GameState> onStateChange;

    void Start()
    {
        StartCoroutine(SetDefaultState());
    }

    public static void SetState(GameState state, bool force = false)
    {
        GameState previousState = currentState;
        if (state != currentState || force)
        {

            currentState = state;
            instance.onStateChange?.Invoke(previousState, state);
        }
        else if (!force)
            Debug.LogWarning($"State is already {state}");

        instance._currentState = state;
    }

    private IEnumerator SetDefaultState()
    {
        yield return new WaitForEndOfFrame();
        SetState(_defaultState, force: true);
    }
}
