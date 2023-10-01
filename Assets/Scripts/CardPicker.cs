using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum PickerAction { Select, AddToDeck }

public class CardPicker : MonoBehaviour
{
    public PickerAction pickerAction = PickerAction.Select;
    public Card card;
    public BoxCollider2D boxCollider;
    public TextMeshPro costText;
    public CardPickerController controller;

    private void OnMouseDown()
    {
        if (card == null) return;

        if (pickerAction == PickerAction.Select)
            SetSelectedCard();
        else if (pickerAction == PickerAction.AddToDeck)
            AddToDeck();
    }

    public void Initialize(Card card)
    {
        this.card = card;
        BlockGroup blockGroup = new BlockGroup(card);
        GameObject newObj = blockGroup.InstantiateGameObject(
            BlockContextManager.instance.GetContextPosition(transform.position)
        );
        newObj.transform.position = transform.position;
        SetColliderBounds(blockGroup);
        costText.text = card.cost.ToString();
    }

    public void SetSelectedCard() => BlockInstanciator.instance.SetSelectedCard(card);
    public void AddToDeck()
    {
        DraftManager.instance.AddToDeck(card);
        controller.RefreshPickers();
    }

    private void SetColliderBounds(BlockGroup blockGroup)
    {
        Bounds bounds = blockGroup.GetBounds();
        boxCollider.size = bounds.size;
        boxCollider.offset = bounds.center;
    }
}
