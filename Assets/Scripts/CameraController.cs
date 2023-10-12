using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float maxOffset = 10f;
    private float _yMin;
    public float scrollScaler = 1f;
    private float _maxHeight;

    void Start()
    {
        _yMin = transform.position.y;
        UpdateMaxHeight();
    }

    void Update()
    {
        float y = transform.position.y + Input.mouseScrollDelta.y * scrollScaler;
        y = Mathf.Clamp(y, _yMin, _maxHeight + maxOffset);
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    public void UpdateMaxHeight()
    {
        _maxHeight = BlockContextManager.instance.currentContext.GetMaxHeight();
    }
}
