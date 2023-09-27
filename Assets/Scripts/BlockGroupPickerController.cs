using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGroupPickerController : MonoBehaviour
{
    BlockGroupPicker[] pickers;
    int selectedPickerIdx = 0;

    private void Start()
    {
        pickers =transform.GetComponentsInChildren<BlockGroupPicker>();
        pickers[0].SetGhostObject();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            selectedPickerIdx = (selectedPickerIdx + 1) % pickers.Length;
            pickers[selectedPickerIdx].SetGhostObject();
        }
    }
}
