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
    public CanvasGroup oppacityCanvasGroup;
    public StatusTextUI statusTextUI;
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
        Fade(1f);
        pickerController.gameObject.SetActive(true);
        remainingDraftCount = drafCount;
        SetStatusText();

        onDraftStart?.Invoke();
    }

    private void EndDraft()
    {
        Fade(0f);
        pickerController.ClearPickers();
        pickerController.gameObject.SetActive(false);
        StateManager.SetState(GameState.TURN);
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

    private void Fade(float targetAlpha)
    {
        if (oppacityCanvasGroup != null)
            LeanTweenUtils.AnimateCanvasGroup(oppacityCanvasGroup, targetAlpha, 0.5f);
    }

    public void SetStatusText()
    {
        if (remainingDraftCount > 0)
            statusTextUI.SetText($"Draft left: {remainingDraftCount}");
        else
            statusTextUI.SetText("");
    }
}
