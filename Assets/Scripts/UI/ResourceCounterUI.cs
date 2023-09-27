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
        ResourceGroup resources = ResourceManager.instance.staticResources;
        goldText.text = $"Gold: {resources.gold}";
        foodText.text = $"Food: {resources.food}";
        peopleText.text = $"People: {resources.people}";
    }
}
