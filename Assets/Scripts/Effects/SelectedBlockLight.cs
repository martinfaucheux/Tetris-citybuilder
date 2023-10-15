using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedBlockLight : MonoBehaviour
{
    [Range(0f, 1f)]
    public float maxAlpha = 0.4f;
    [Range(0f, 2f)]
    public float pulseDuration = 0.5f;
    public SpriteRenderer spriteRenderer;
    private int _ltDescrId = 0;
    private bool isPulsing = false;
    public float zlightDisp = 1f;
    private BlockHolder _blockHolder;

    void Start()
    {
        if (!isPulsing)
            spriteRenderer.color = GetColor(false);
    }

    void Update()
    {
        FollorBlockHolder();
    }

    public void StartPulse()
    {
        spriteRenderer.color = GetColor(false);
        isPulsing = true;
        LTDescr ltDescr = (
            LeanTween.color(gameObject, GetColor(true), pulseDuration)
            .setLoopPingPong()
            .setEaseInOutSine()
            .setOnComplete(() => isPulsing = false)
        );
        _ltDescrId = ltDescr.id;
    }

    private Color GetColor(bool activated)
    {
        Color color = spriteRenderer.color;
        color.a = activated ? maxAlpha : 0f;
        return color;
    }

    public void StopPulse()
    {
        if (isPulsing)
        {
            LeanTween.cancel(_ltDescrId);
            Color color = spriteRenderer.color;
            color.a = 0f;
            spriteRenderer.color = color;
        }
    }

    public void Initialize(BlockHolder blockHolder)
    {
        _blockHolder = blockHolder;
        transform.position = blockHolder.transform.position + new Vector3(0f, 0f, -zlightDisp);
        FollorBlockHolder();
    }

    private void FollorBlockHolder()
    {
        if (_blockHolder != null)
            transform.position = _blockHolder.transform.position + new Vector3(0f, 0f, -zlightDisp);
    }
}
