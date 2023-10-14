using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HeightCounterUI : MonoBehaviour
{
    public TextMeshProUGUI heightText;

    public void Refresh()
    {
        int maxHeight = BlockContextManager.instance.currentContext.GetMaxHeight();
        heightText.text = maxHeight.ToString() + "m";
    }
}
