using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodTextDisplay : MonoBehaviour
{
    public TextMeshProUGUI foodText;

    void Start()
    {
        Refresh();
        ResourceManager.instance.onFoodChange += Refresh;
    }

    private void OnDestroy()
    {
        ResourceManager.instance.onFoodChange -= Refresh;
    }

    public void Refresh()
    {
        int foodAmount = ResourceManager.instance.foodAmount;
        foodText.text = $"Food: {foodAmount}";
    }
}
