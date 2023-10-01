using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StatusTextUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public CanvasGroup canvasGroup;
    private int ltDescrId;
    public float duration = 3f;
    void Start()
    {
        canvasGroup.alpha = 0;
    }


    public void SetText(string text, Color color)
    {
        canvasGroup.alpha = 1;
        this.text.text = TextUtils.Colorize(text, color);
        FadeOut();
    }
    public void SetText(string text) => SetText(text, Color.white);

    private void FadeOut()
    {
        LeanTween.cancel(ltDescrId);
        LTDescr ltDescr = LeanTweenUtils.AnimateCanvasGroup(
            canvasGroup, 0, duration
        ).setEaseInQuint();
        ltDescrId = ltDescr.id;
    }
}
