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
        DeckManager.instance.onDeckChanged += RefreshPickers;
    }

    void OnDestroy()
    {
        DeckManager.instance.onDeckChanged -= RefreshPickers;
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

    protected override int GetCardCount() => DeckManager.instance.cardList.Count;

    protected override Card GetCard(int index)
    {
        return DeckManager.instance.cardList[index];
    }

    public override void PickCard(Card card)
    {
        BlockInstanciator.instance.SetSelectedCard(card);
    }
}
