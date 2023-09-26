using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PeopleTextDisplay : MonoBehaviour
{
    public TextMeshProUGUI peopleText;

    void Start()
    {
        Refresh();
        ResourceManager.instance.onPeopleChange += Refresh;
    }

    private void OnDestroy()
    {
        ResourceManager.instance.onPeopleChange -= Refresh;
    }

    public void Refresh()
    {
        int peopleAmount = ResourceManager.instance.peopleAmount;
        peopleText.text = $"People: {peopleAmount}";
    }
}
