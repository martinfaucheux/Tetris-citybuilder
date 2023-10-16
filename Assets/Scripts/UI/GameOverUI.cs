using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup.alpha = 0f;
        StateManager.instance.onStateChange += OnStateChange;
    }

    void OnDestroy()
    {
        StateManager.instance.onStateChange -= OnStateChange;
    }

    private void OnStateChange(GameState previousState, GameState newState)
    {
        if (newState == GameState.GAMEOVER)
            LeanTweenUtils.AnimateCanvasGroup(canvasGroup, 1f, 0.5f);
    }
}
