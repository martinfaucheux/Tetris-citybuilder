using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockInstanciator : Singleton<BlockInstanciator>
{
    public GameObject blockGroupPrefab;
    public float moveCooldown = 0.1f;
    public int maxIteriation = 10000;

    private BlockGroup blockGroup;


    float _lastMoveTime = 0f;

    public Vector2Int matrixPosition { get => CollisionMatrix.instance.GetMatrixPos(transform); }

    void Start()
    {
        SetGhostObject(blockGroupPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < _lastMoveTime + moveCooldown)
            return;

        int disp = 0;
        if (Input.GetKeyDown(KeyCode.Q))
            disp = -1;
        else if (Input.GetKeyDown(KeyCode.D))
            disp = 1;

        int rot = 0;
        if (Input.GetKeyDown(KeyCode.A))
            rot = -1;
        else if (Input.GetKeyDown(KeyCode.E))
            rot = 1;

        if (rot != 0)
        {
            transform.Rotate(0, 0, rot * 90);
            blockGroup.SynchronizePosition();
            if (!blockGroup.IsValidPosition(matrixPosition))
            {
                transform.Rotate(0, 0, -rot * 90);
                blockGroup.SynchronizePosition();
            }
        }

        if (disp != 0)
        {
            Vector2Int newPos = matrixPosition + new Vector2Int(disp, 0);
            if (blockGroup.IsValidPosition(newPos))
            {
                transform.position = CollisionMatrix.instance.GetRealWorldPosition(newPos);
                blockGroup.SynchronizePosition();
                _lastMoveTime = Time.time;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (ResourceManager.instance.CanAfford(blockGroup.cost))
            {
                Vector2Int spawnPos = blockGroup.GetLowestPosition(matrixPosition.x);
                Spawn(spawnPos);
            }
            else
                Debug.Log("Not enough resources");
        }
    }

    private void Spawn(Vector2Int _matrixPosition)
    {
        Vector3 position = CollisionMatrix.instance.GetRealWorldPosition(_matrixPosition);
        GameObject newObj = Instantiate(blockGroup.gameObject, position, transform.rotation);
        newObj.GetComponent<BlockGroup>().Place();
        TurnManager.instance.StartNewTurn();
    }


    public void SetGhostObject(GameObject blockGroupObj)
    {
        transform.rotation = Quaternion.identity;
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        GameObject ghostObject = Instantiate(blockGroupObj, transform.position, Quaternion.identity, transform);
        blockGroup = ghostObject.GetComponent<BlockGroup>();
    }
}
