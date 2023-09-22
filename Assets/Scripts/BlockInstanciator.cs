using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockInstanciator : MonoBehaviour
{
    public GameObject blockPrefab;

    public float moveCooldown = 0.1f;
    float _lastMoveTime = 0f;

    public Vector2Int matrixPosition { get => CollisionMatrix.instance.GetMatrixPos(transform); }

    // Start is called before the first frame update
    void Start()
    {
        
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

        if (disp != 0)
        {
            Vector2Int newPos = matrixPosition + new Vector2Int(disp, 0);
            if (Mathf.Abs(newPos.x) <= CollisionMatrix.instance.maxX)
            {
                transform.position = CollisionMatrix.instance.GetRealWorldPosition(newPos);
                _lastMoveTime = Time.time;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2Int? spawnPos = GetSpawnPosition();
            if (spawnPos != null)
            {
                Spawn((Vector2Int)spawnPos);
            }
        }   
    }

    private void Spawn(Vector2Int _matrixPosition)
    {
        Vector3 position = CollisionMatrix.instance.GetRealWorldPosition(_matrixPosition);
        Instantiate(blockPrefab, position, Quaternion.identity);
    }

    public Vector2Int? GetSpawnPosition()
    {

        for (int y = 0; y< matrixPosition.y; y++)
        {
            Vector2Int spawnPos = new Vector2Int(matrixPosition.x, y);
            if (
                CollisionMatrix.instance.IsValidPosition(spawnPos)
                && !CollisionMatrix.instance.GetObjectsAtPosition(spawnPos).Any()
            )
            {
                return spawnPos;   
            }
        }
        return null;
    }
}
