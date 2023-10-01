using System;
using UnityEngine;

public enum GameState { TURN, DRAFT, GAMEOVER }

public class StateManager : Singleton<StateManager>
{
    public static GameState currentState { get; private set; }
    public event Action<GameState, GameState> onStateChange;

    void Start()
    {
        SetState(GameState.TURN, force: true);
    }

    public static void SetState(GameState state, bool force = false)
    {
        GameState previousState = currentState;
        if (state != currentState)
        {

            currentState = state;
            instance.onStateChange?.Invoke(previousState, state);
        }
        else if (!force)
            Debug.LogWarning($"State is already {state}");


    }
}
