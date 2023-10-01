using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardPickerController : MonoBehaviour
{
    public GameObject pickerPrefab;
    public float pickerMaxX = 5f;
    List<CardPicker> pickers = new List<CardPicker>();
    int selectedPickerIdx = -1;

    private void Start()
    {
        pickers = GetComponentsInChildren<CardPicker>().ToList();
        RefreshPickers();
    }

    private void Update()
    {
        if (StateManager.currentState == GameState.TURN && Input.GetKeyDown(KeyCode.Tab))
        {
            if (pickers.Count == 0)
                selectedPickerIdx = -1;
            else
            {
                selectedPickerIdx = (selectedPickerIdx + 1) % pickers.Count;
                pickers[selectedPickerIdx].SetSelectedCard();
            }
        }
    }

    public void RefreshPickers()
    {
        int cardCount = DeckManager.instance.cardList.Count;
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
            Card card = DeckManager.instance.cardList[cardIndex];
            CardPicker cardPicker;

            if (cardIndex >= pickers.Count)
            {
                GameObject pickerObj = Instantiate(
                    pickerPrefab, GetPickerPosition(cardIndex), Quaternion.identity, transform
                );
                cardPicker = pickerObj.GetComponent<CardPicker>();
                cardPicker.controller = this;
            }
            else
            {
                cardPicker = pickers[cardIndex];
                cardPicker.transform.position = GetPickerPosition(cardIndex);
            }
            cardPicker.Initialize(card);
            pickers.Add(cardPicker);
        }
    }

    private Vector3 GetPickerPosition(int cardIndex)
    {
        float disp = pickerMaxX * 2 * ((float)cardIndex / (DeckManager.instance.cardList.Count - 1) - 0.5f);
        return transform.position + new Vector3(disp, 0f, 0f);
    }
}
