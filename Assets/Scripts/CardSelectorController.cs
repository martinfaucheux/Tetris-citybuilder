using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSelectorController : BaseCardPickerController
{
    int selectedPickerIdx = -1;

    protected override void Start()
    {
        base.Start();
        StateManager.instance.onStateChange += OnStateChange;
    }

    private void OnDestroy()
    {
        StateManager.instance.onStateChange -= OnStateChange;
    }

    protected override void Update()
    {
        base.Update();
        if (
            StateManager.currentState == GameState.TURN
            && Input.GetKeyDown(KeyCode.Tab)
        )
        {
            if (pickers.Count == 0)
                selectedPickerIdx = -1;
            else
            {
                selectedPickerIdx = (selectedPickerIdx + 1) % pickers.Count;
                PickCard(pickers[selectedPickerIdx].card);
            }
        }
    }

    private void OnStateChange(GameState previous, GameState current)
    {
        // TODO: instantiated blocks must be child of this gameobject

        // if (current == GameState.DRAFT)
        //     gameObject.SetActive(false);
        // else if (previous == GameState.DRAFT)
        //     gameObject.SetActive(true);
    }

    protected override int GetCardCount() => DeckManager.instance.handCards.Count;

    protected override Card GetCard(int index)
    {
        return DeckManager.instance.handCards[index];
    }

    public override void PickCard(Card card)
    {
        BlockInstanciator.instance.SetSelectedCard(card);
    }

    public void SelectFirst()
    {
        if (pickers.Count > 0)
        {
            selectedPickerIdx = 0;
            PickCard(pickers[0].card);
        }
        else
        {
            selectedPickerIdx = -1;
            PickCard(null);
        }
    }
}
