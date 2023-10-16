using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseCardPickerController : MonoBehaviour
{
    public GameObject pickerPrefab;
    public float spacing = 3f;
    protected List<CardHolder> pickers = new List<CardHolder>();
    public LayerMask layerMask;
    public RectTransform virtualCardHolder;

    protected virtual void Start()
    {
        pickers = GetComponentsInChildren<CardHolder>().ToList();
        RefreshPickers();
    }

    protected abstract Card GetCard(int index);
    protected abstract int GetCardCount();
    public abstract void PickCard(Card card);


    protected virtual void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;

            Vector3 rayPosition = Camera.main.ScreenToWorldPoint(mousePos);

            RaycastHit2D hit = Physics2D.Raycast(rayPosition, Vector2.zero, Mathf.Infinity, layerMask);
            if (hit.collider != null)
            {
                CardHolder picker = hit.collider.GetComponent<CardHolder>();
                if (picker != null && pickers.Contains(picker))
                {
                    PickCard(picker.card);
                }
            }
        }
    }
    public void RefreshPickers()
    {
        int cardCount = GetCardCount();
        if (pickers.Count > cardCount)
        {
            for (int pickerIndex = cardCount; pickerIndex < pickers.Count; pickerIndex++)
            {
                Destroy(pickers[pickerIndex].gameObject);
            }
            pickers.RemoveRange(cardCount, pickers.Count - cardCount);
        }

        for (int cardIndex = 0; cardIndex < cardCount; cardIndex++)
        {
            Card card = GetCard(cardIndex);
            CardHolder cardPicker;

            if (cardIndex >= pickers.Count)
            {
                cardPicker = InstantiatePicker(cardIndex);
                pickers.Add(cardPicker);
            }
            else
            {
                cardPicker = pickers[cardIndex];
            }
            cardPicker.transform.position = GetPickerPosition(cardIndex);
            cardPicker.transform.rotation = GetPickerRotation(cardIndex);
            cardPicker.Initialize(card);
        }
    }

    protected virtual CardHolder InstantiatePicker(int cardIndex)
    {
        GameObject pickerObj = Instantiate(pickerPrefab, transform);
        pickerObj.name = $"Picker {cardIndex}";
        CardHolder cardHolder = pickerObj.GetComponent<CardHolder>();
        if (virtualCardHolder != null)
        {
            RectTransform uiEquivalent = GetUiEquivalent(cardIndex);
            Vector3 scale = UiUtils.GetUiRealWordScale(uiEquivalent);
            cardHolder.transform.localScale = scale;
        }
        return cardHolder;
    }

    protected virtual Vector3 GetPickerPosition(int cardIndex)
    {
        if (virtualCardHolder != null)
        {
            RectTransform uiEquivalent = GetUiEquivalent(cardIndex);
            if (uiEquivalent != null)
            {
                RectTransform uiElement = (RectTransform)virtualCardHolder.GetChild(cardIndex);
                Vector3 uiPosition = uiElement.TransformPoint(uiElement.rect.center);
                Vector3 worldPosition;
                if (RectTransformUtility.ScreenPointToWorldPointInRectangle(uiElement, uiPosition, Camera.main, out worldPosition))
                    return worldPosition;
                else
                    Debug.LogError("Failed to convert UI position to world position");
            }
        }

        // legacy code
        int cardCount = GetCardCount();
        if (cardCount == 1)
            return transform.position;

        float disp = spacing * (cardIndex - (cardCount - 1) * 0.5f);
        return transform.position + new Vector3(disp, 0f, 0f);
    }

    protected virtual Quaternion GetPickerRotation(int cardIndex) => Quaternion.identity;

    public void ClearPickers()
    {
        foreach (CardHolder picker in pickers)
            Destroy(picker.gameObject);
    }

    protected RectTransform GetUiEquivalent(int cardIndex)
    {
        if (cardIndex >= virtualCardHolder.childCount)
        {
            Debug.LogError("Card index out of range");
            return null;
        }
        return (RectTransform)virtualCardHolder.GetChild(cardIndex);
    }

}
