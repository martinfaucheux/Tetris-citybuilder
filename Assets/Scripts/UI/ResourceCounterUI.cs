using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceCounterUI : MonoBehaviour
{
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI peopleText;
    public Color positiveColor = Color.green;
    public Color negativeColor = Color.red;

    public void Refresh()
    {
        ResourceGroup resources = ResourceManager.instance.staticResources;
        ResourceGroup upkeep = ResourceManager.instance.GetUpkeep();

        string goldText = $"Gold: {resources.gold}";
        if (upkeep.gold != 0)
        {
            string symbol = upkeep.gold > 0 ? "+" : "-";
            Color color = upkeep.gold > 0 ? positiveColor : negativeColor;
            goldText += TextUtils.Colorize($" {symbol}{Mathf.Abs(upkeep.gold)}", color);
        }
        this.goldText.text = goldText;
        foodText.text = $"Food: {resources.food}";
        peopleText.text = $"People: {resources.people}";
    }

}
