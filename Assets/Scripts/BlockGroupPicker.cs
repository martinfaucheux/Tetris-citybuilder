using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGroupPicker : MonoBehaviour
{
    private void OnMouseDown()
    {
        SetGhostObject();
    }

    public void SetGhostObject() => BlockInstanciator.instance.SetGhostObject(transform.GetChild(0).gameObject);
}
