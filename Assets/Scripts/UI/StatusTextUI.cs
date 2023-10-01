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
    public void SetText(string text)
    {
        canvasGroup.alpha = 1;
        this.text.text = text;
        FadeOut();
    }

    private void FadeOut()
    {
        LeanTween.cancel(ltDescrId);
        LTDescr ltDescr = LeanTween.value(
            gameObject,
            (float val) => canvasGroup.alpha = val,
            1,
            0,
            duration
        ).setEaseInQuint();
        ltDescrId = ltDescr.id;
    }
}
