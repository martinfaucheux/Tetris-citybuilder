using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGroupPicker : MonoBehaviour
{
    public Card card;
    public BoxCollider2D boxCollider;

    void Start()
    {
        BlockGroup blockGroup = new BlockGroup(card);
        GameObject newObj = blockGroup.InstantiateGameObject(
            BlockContextManager.instance.GetContextPosition(transform.position)
        );
        newObj.transform.position = transform.position;
        SetColliderBounds(blockGroup);
    }

    private void OnMouseDown()
    {
        SetSelectedCard();
    }

    public void SetSelectedCard() => BlockInstanciator.instance.SetSelectedCard(card);

    public void SetColliderBounds(BlockGroup blockGroup)
    {
        int xMin = int.MaxValue;
        int xMax = int.MinValue;
        int yMin = int.MaxValue;
        int yMax = int.MinValue;
        foreach (Vector2Int relativePosition in blockGroup.grid)
        {
            xMin = Mathf.Min(xMin, relativePosition.x);
            xMax = Mathf.Max(xMax, relativePosition.x);
            yMin = Mathf.Min(yMin, relativePosition.y);
            yMax = Mathf.Max(yMax, relativePosition.y);
        }

        boxCollider.size = new Vector2(1 + xMax - xMin, 1 + yMax - yMin);
        boxCollider.offset = new Vector2((xMax + xMin) / 2f, (yMax + yMin) / 2f);
    }
}
