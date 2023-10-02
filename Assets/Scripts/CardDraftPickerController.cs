using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDraftPickerController : BaseCardPickerController
{
    protected override int GetCardCount() => DraftManager.instance.draftOptionCount;
    protected override Card GetCard(int index)
    {
        return CardForge.instance.GenerateCard();
    }

    public override void PickCard(Card card)
    {
        DraftManager.instance.AddToDeck(card);
        RefreshPickers();
    }
}
