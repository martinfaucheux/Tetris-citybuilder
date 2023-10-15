using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockHolderState
{
    NONE,   // default state
    CARD,   // blocks are attached to a card (hand, draft...)
    GHOST,  // block is not yet registered but is displayed in game
    PLACED, // block is registered
}

public class BlockHolder : MonoBehaviour
{
    public Block block;
    [Tooltip("This is mostly for debugging purposes")]
    public BlockData blockData;
    [field: SerializeField]
    public BlockHolderState state { get; private set; } = BlockHolderState.NONE;
    public string objectPoolCode = "SelectedBlockLight";
    private SelectedBlockLight _blockLight;

    void Start()
    {
        SetBlockLight();
    }

    private void LateUpdate()
    {
        // sprite should never be rotated
        transform.rotation = Quaternion.identity;
    }

    public void SetState(BlockHolderState newState)
    {
        BlockHolderState previousState = state;
        if (newState == previousState)
            return;

        state = newState;

        if (newState == BlockHolderState.GHOST)
            StartCoroutine(SetPulse(true));

        if (previousState == BlockHolderState.GHOST)
            StartCoroutine(SetPulse(false));
    }

    private IEnumerator SetPulse(bool activate)
    {
        // Done through coroutine as need to be played after Start method
        yield return new WaitForEndOfFrame();
        if (activate)
            _blockLight.StartPulse();
        else
            _blockLight.StopPulse();
    }

    private void SetBlockLight()
    {
        if (_blockLight != null)
            return;

        _blockLight = PoolManager.GetPool(objectPoolCode).GetPooledObject().GetComponent<SelectedBlockLight>();
        _blockLight.Initialize(this);
    }
}
