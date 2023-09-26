using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockInstanciator : MonoBehaviour
{
    public GameObject blockGroupPrefab;
    public float moveCooldown = 0.1f;
    public int maxIteriation = 10000;

    private BlockGroup blockGroup;
    

    float _lastMoveTime = 0f;

    public Vector2Int matrixPosition { get => CollisionMatrix.instance.GetMatrixPos(transform); }

    void Start()
    {
        GameObject ghostObject = Instantiate(blockGroupPrefab, transform.position, Quaternion.identity, transform);
        blockGroup = ghostObject.GetComponent<BlockGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < _lastMoveTime + moveCooldown)
            return;

        int disp = 0;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            disp = -1;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            disp = 1;

        int rot = 0;
        if (Input.GetKeyDown(KeyCode.UpArrow))
            rot = -1;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
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

        if (
            Input.GetKeyDown(KeyCode.Space)
            && ResourceManager.instance.CanAfford(blockGroup.cost)
            )
        {
            Vector2Int? spawnPos = GetSpawnPosition();
            if (spawnPos == null) {
                Debug.LogError("Max iteration reached");
            } else {
                Spawn((Vector2Int)spawnPos);
            }
        }   
    }

    private void Spawn(Vector2Int _matrixPosition)
    {
        Vector3 position = CollisionMatrix.instance.GetRealWorldPosition(_matrixPosition);
        ResourceManager.instance.Add(-blockGroup.cost);
        GameObject newObj = Instantiate(blockGroupPrefab, position, transform.rotation);
        newObj.GetComponent<BlockGroup>().Place();
        TurnManager.instance.StartNewTurn();
    }

    public Vector2Int? GetSpawnPosition()
    {
        for (int y = 0; y < maxIteriation; y ++ )
        {
            Vector2Int basePosition = new Vector2Int(matrixPosition.x, y);
            if (!blockGroup.IsValidPosition(basePosition))
                continue;

            return basePosition;
        }
        return null;
    }
}
