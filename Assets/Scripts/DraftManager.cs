using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DraftManager : Singleton<DraftManager>
{
    [field: Range(1, 10)]
    [field: SerializeField]
    public int totalDraftCount { get; private set; } = 3;
    public int draftOptionCount { get; private set; } = 3;
    [field: SerializeField]
    public int remainingDraftCount { get; private set; } = 0;
    public CardDraftPickerController pickerController;
    public StatusTextUI statusTextUI;
    public bool autoDraft;
    public UnityEvent onDraftStart;

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
    }

    private void StartDraft() => StartDraft(totalDraftCount);
    private void StartDraft(int drafCount)
    {
        pickerController.gameObject.SetActive(true);
        remainingDraftCount = drafCount;
        SetStatusText();

        onDraftStart?.Invoke();

        if (autoDraft)
        {
            for (int cardIndex = 0; cardIndex < draftOptionCount; cardIndex++)
                AddToDeck(CardForge.instance.GenerateCard());
        }
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
        SetStatusText();

        if (remainingDraftCount <= 0)
            EndDraft();
    }

    private void EndDraft()
    {
        if (StateManager.currentState != GameState.DRAFT)
        {
            Debug.LogError("Can't end draft if not in draft");
            return;
        }
        // TODO: fix randomize first hand

        DeckManager.instance.Reset();
        DeckManager.instance.DrawHand();
        pickerController.ClearPickers();
        pickerController.gameObject.SetActive(false);
        StateManager.SetState(GameState.TURN);
    }

    public void SetStatusText()
    {
        if (remainingDraftCount > 0)
            statusTextUI.SetText($"Draft left: {remainingDraftCount}");
        else
            statusTextUI.SetText("");
    }
}
