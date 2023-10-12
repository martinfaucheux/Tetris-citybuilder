using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float maxOffset = 10f;
    private float _yMin;
    public float scrollScaler = 1.5f;
    public float buttonScaler = 3f;
    private float _maxHeight;

    void Start()
    {
        _yMin = transform.position.y;
        UpdateMaxHeight();
    }

    void Update()
    {
        float disp;

        float buttonInput = GetArrowMovement() * buttonScaler * Time.deltaTime;
        float scrollInput = Input.mouseScrollDelta.y * scrollScaler;
        if (buttonInput != 0)
            disp = buttonInput;
        else if (scrollInput != 0)
            disp = scrollInput;
        else
            return;

        float y = transform.position.y + disp;
        y = Mathf.Clamp(y, _yMin, _maxHeight + maxOffset);
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    public void UpdateMaxHeight()
    {
        _maxHeight = BlockContextManager.instance.currentContext.GetMaxHeight();
    }

    private float GetArrowMovement()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            return 1;
        else if (Input.GetKey(KeyCode.DownArrow))
            return -1;
        return 0;
    }
}
