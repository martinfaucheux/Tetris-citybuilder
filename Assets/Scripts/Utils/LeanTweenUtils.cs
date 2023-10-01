using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LeanTweenUtils
{
    public static LTDescr AnimateCanvasGroup(
        CanvasGroup canvasGroup,
        float to,
        float duration
    ) => LeanTween.value(
        canvasGroup.gameObject,
        (float val) => canvasGroup.alpha = val,
        canvasGroup.alpha,
        to,
        duration
    );
}
