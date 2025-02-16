using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CornerBadgeUI : MonoBehaviour
{
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI taxText;
    public Button endDayButton;
    public Transform pointContainer;
    [Range(0, 1)]
    public float disablePointScale = 0.7f;

    void Start()
    {
        StateManager.instance.onStateChange += OnStateChange;
    }

    void OnDestroy()
    {
        StateManager.instance.onStateChange -= OnStateChange;
    }

    private void OnStateChange(GameState previousState, GameState newState)
    {
        endDayButton.interactable = newState == GameState.TURN;
    }

    public void Refresh()
    {
        dayText.text = $"Day {TurnManager.instance.turnCount}";
        taxText.text = $"Tax: <sprite name=\"Gold\"> {ResourceManager.instance.GetTaxAmount()}";

        int taxCycle = ResourceManager.instance.taxCycleDuration;
        if (pointContainer.childCount != taxCycle)
            Debug.LogError("Invalid child count");

        int cycleDayCount = TurnManager.instance.turnCount % taxCycle;
        for (int childIdx = 0; childIdx < taxCycle; childIdx++)
        {
            Transform pointTransform = pointContainer.GetChild(childIdx);
            pointTransform.localScale = (childIdx < cycleDayCount ? 1f : disablePointScale) * Vector3.one;
        }
    }
}
