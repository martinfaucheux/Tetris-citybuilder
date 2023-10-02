using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseCardPickerController : MonoBehaviour
{
    public GameObject pickerPrefab;
    public float pickerMaxX = 5f;
    protected List<CardPicker> pickers = new List<CardPicker>();
    public LayerMask layerMask;

    protected virtual void Start()
    {
        pickers = GetComponentsInChildren<CardPicker>().ToList();
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
            Debug.DrawRay(rayPosition, Vector2.zero, Color.red, 5f);

            RaycastHit2D hit = Physics2D.Raycast(rayPosition, Vector2.zero, Mathf.Infinity, layerMask);
            if (hit.collider != null)
            {
                CardPicker picker = hit.collider.GetComponent<CardPicker>();
                if (picker != null && pickers.Contains(picker))
                {
                    PickCard(picker.card);
                }
            }
        }
    }
    protected void RefreshPickers()
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
            CardPicker cardPicker;

            if (cardIndex >= pickers.Count)
            {
                GameObject pickerObj = Instantiate(pickerPrefab, transform);
                pickerObj.name = $"Picker {cardIndex}";
                cardPicker = pickerObj.GetComponent<CardPicker>();
                cardPicker.controller = this;
                pickers.Add(cardPicker);
            }
            else
            {
                cardPicker = pickers[cardIndex];
            }
            cardPicker.transform.position = GetPickerPosition(cardIndex);
            cardPicker.Initialize(card);
        }
    }

    private Vector3 GetPickerPosition(int cardIndex)
    {
        int cardCount = GetCardCount();
        if (cardCount == 1)
            return transform.position;

        float disp = pickerMaxX * 2 * ((float)cardIndex / (cardCount - 1) - 0.5f);
        return transform.position + new Vector3(disp, 0f, 0f);
    }

    public void ClearPickers()
    {
        foreach (CardPicker picker in pickers)
            Destroy(picker.gameObject);

    }
}
