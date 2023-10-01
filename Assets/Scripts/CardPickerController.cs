using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPickerController : MonoBehaviour
{
    CardPicker[] pickers;
    int selectedPickerIdx = -1;

    private void Start()
    {
        pickers = transform.GetComponentsInChildren<CardPicker>();
    }

    private void Update()
    {
        if (StateManager.currentState == GameState.TURN && Input.GetKeyDown(KeyCode.Tab))
        {
            selectedPickerIdx = (selectedPickerIdx + 1) % pickers.Length;
            pickers[selectedPickerIdx].SetSelectedCard();
        }
    }
}
