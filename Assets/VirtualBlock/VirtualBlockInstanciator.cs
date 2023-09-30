using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VirtualBlockInstanciator : Singleton<VirtualBlockInstanciator>
{

    public Card selectedCard;
    public VirtualBlockGroup blockGroup;
    public GameObject ghostGameObject;
    public float moveCooldown = 0.1f;
    public int maxIteriation = 10000;
    float _lastMoveTime = 0f;
    Vector2Int instantiatePosition;

    void Start()
    {
        instantiatePosition = BlockContextManager.instance.GetContextPosition(transform.position);
        RefreshGhostObject();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int groupContextPosition = BlockContextManager.instance.GetContextPosition(blockGroup.transform.position);

        if (Time.time < _lastMoveTime + moveCooldown)
            return;

        int disp = 0;
        if (Input.GetKeyDown(KeyCode.Q))
            disp = -1;
        else if (Input.GetKeyDown(KeyCode.D))
            disp = 1;

        // int rot = 0;
        // if (Input.GetKeyDown(KeyCode.A))
        //     rot = -1;
        // else if (Input.GetKeyDown(KeyCode.E))
        //     rot = 1;

        // if (rot != 0)
        // {
        //     transform.Rotate(0, 0, rot * 90);
        //     blockGroup.SynchronizePosition();
        //     if (!blockGroup.IsValidPosition(matrixPosition))
        //     {
        //         transform.Rotate(0, 0, -rot * 90);
        //         blockGroup.SynchronizePosition();
        //     }
        // }

        if (disp != 0)
        {
            Vector2Int newPos = groupContextPosition + new Vector2Int(disp, 0);
            if (blockGroup.IsValidPosition(BlockContextManager.instance.currentContext, newPos))
            {
                instantiatePosition = newPos;
                blockGroup.Move(newPos);
                _lastMoveTime = Time.time;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (ResourceManager.instance.CanAfford(blockGroup.cost))
            {
                Vector2Int spawnPos = blockGroup.GetLowestPosition(BlockContextManager.instance.currentContext, groupContextPosition.x);
                Spawn(spawnPos);
            }
            else
                // TODO: explain which resource is missing
                Debug.Log("Not enough resources");
        }
    }

    private void Spawn(Vector2Int contextPosition)
    {
        // this assume the matrixPosition is valid

        // update the contextPosition and transform positions
        blockGroup.Move(contextPosition);

        // blocks are registered in the Context
        blockGroup.Register();

        ghostGameObject.transform.SetParent(null);
        RefreshGhostObject();
    }


    public void RefreshGhostObject()
    {
        // remove existing object
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        blockGroup = new VirtualBlockGroup(selectedCard);
        ghostGameObject = blockGroup.InstantiateGameObject(instantiatePosition);
    }


}
