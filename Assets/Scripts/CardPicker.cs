using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardPicker : MonoBehaviour
{
    public Card card;
    public BoxCollider2D boxCollider;
    public TextMeshPro costText;
    public BaseCardPickerController controller;
    public GameObject instantiatedObject;

    public void Initialize(Card card)
    {
        if (instantiatedObject != null)
            Destroy(instantiatedObject);

        this.card = card;
        BlockGroup blockGroup = new BlockGroup(card);
        instantiatedObject = blockGroup.InstantiateGameObject(
            BlockContextManager.instance.GetContextPosition(transform.position)
        );
        instantiatedObject.transform.position = transform.position;
        SetColliderBounds(blockGroup);
        costText.text = card.cost.ToString();
    }

    private void OnDestroy()
    {
        if (instantiatedObject != null)
            Destroy(instantiatedObject);
    }

    private void SetColliderBounds(BlockGroup blockGroup)
    {
        Bounds bounds = blockGroup.GetBounds();
        boxCollider.size = bounds.size;
        boxCollider.offset = bounds.center;
    }
}
