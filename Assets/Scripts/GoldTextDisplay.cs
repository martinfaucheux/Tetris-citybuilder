using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldTextDisplay : MonoBehaviour
{
    public TextMeshProUGUI goldText;

    void Start()
    {
        Refresh();
        ResourceManager.instance.onGoldChange += Refresh;
    }

    private void OnDestroy()
    {
        ResourceManager.instance.onGoldChange -= Refresh;
    }

    public void Refresh()
    {
        int goldAmount = ResourceManager.instance.goldAmount;
        goldText.text = $"Gold: {goldAmount}";
    }
}
