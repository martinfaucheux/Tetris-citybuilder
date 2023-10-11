using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CardHolder : MonoBehaviour
{
    public Card card;
    public GameObject blockGroupObject;
    public BlockGroup blockGroup;
    public Transform blockGroupContainer;
    public TextMeshPro costText;

    public void Initialize(Card card)
    {
        if (card == null)
        {
            Debug.LogError("Card is null");
            return;
        }

        if (blockGroupObject != null)
            Destroy(blockGroupObject);

        this.card = card;
        BlockGroup blockGroup = new BlockGroup(card);
        blockGroupObject = blockGroup.InstantiateGameObject(
            BlockContextManager.instance.GetContextPosition(transform.position)
        );
        blockGroupObject.transform.SetParent(blockGroupContainer);
        blockGroupObject.transform.position = blockGroupContainer.position;
        blockGroupObject.transform.localScale = Vector3.one;

        costText.text = card.cost.ToStringIcon();
    }
}
