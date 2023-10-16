using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UiUtils
{
    public static Vector3 GetUiRealWordScale(RectTransform uiElement)
    {
        Vector3[] corners = new Vector3[4];
        // It starts bottom left and rotates to top left, then top right, and finally bottom right
        uiElement.GetWorldCorners(corners);

        float width = (Camera.main.ScreenToWorldPoint(corners[3]) - Camera.main.ScreenToWorldPoint(corners[0])).x;
        float height = (Camera.main.ScreenToWorldPoint(corners[1]) - Camera.main.ScreenToWorldPoint(corners[0])).y;

        return new Vector3(width, height, 1);
    }
}
