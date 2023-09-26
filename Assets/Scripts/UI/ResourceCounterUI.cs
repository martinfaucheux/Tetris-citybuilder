using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceCounterUI : MonoBehaviour
{
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI peopleText;

    public void Refresh()
    {
        ResourceGroup resources = ResourceManager.instance.resources;
        goldText.text = $"Gold: {resources.gold}";
        foodText.text = $"Gold: {resources.food}";
        peopleText.text = $"Gold: {resources.people}";
    }
}
