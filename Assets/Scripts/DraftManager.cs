using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraftManager : Singleton<DraftManager>
{
    [field: Range(1, 10)]
    [field: SerializeField]
    public int initDraftCount { get; private set; } = 3;
    [field: SerializeField]
    public int remainingDraftCount { get; private set; } = 0;
    public CanvasGroup oppacityCanvasGroup;

    void Start()
    {
        StateManager.instance.onStateChange += OnStateChange;
    }

    void OnDestroy()
    {
        StateManager.instance.onStateChange -= OnStateChange;
    }

    public void OnStateChange(GameState previous, GameState current)
    {
        if (current == GameState.DRAFT)
            StartDraft();

        if (previous == GameState.DRAFT)
            LeanTweenUtils.AnimateCanvasGroup(oppacityCanvasGroup, 0, 0.5f);
    }

    private void StartDraft() => StartDraft(initDraftCount);
    private void StartDraft(int drafCount)
    {
        LeanTweenUtils.AnimateCanvasGroup(oppacityCanvasGroup, 1, 0.5f);
        remainingDraftCount = drafCount;
    }

    public void AddToDeck(Card card)
    {
        if (StateManager.currentState != GameState.DRAFT)
        {
            Debug.LogError("Can't add to deck if not in draft");
            return;
        }

        if (remainingDraftCount <= 0)
        {
            Debug.LogError("Can't add to deck if no draft remaining");
            return;
        }

        remainingDraftCount--;
        DeckManager.instance.AddCard(card);
    }
}
