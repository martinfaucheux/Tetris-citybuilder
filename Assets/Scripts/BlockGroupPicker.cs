using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGroupPicker : MonoBehaviour
{
    public bool isDefault = false;

    private void Start()
    {
        if (isDefault)
            SetGhostObject();
    }
    private void OnMouseDown()
    {
        SetGhostObject();
    }

    private void SetGhostObject() => BlockInstanciator.instance.SetGhostObject(transform.GetChild(0).gameObject);
}
