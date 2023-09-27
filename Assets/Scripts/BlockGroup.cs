using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.Build;
using UnityEngine;

public class BlockGroup : MonoBehaviour
{
    public ResourceGroup cost { get => blocks.Select(b => b.cost).Aggregate((x,y) => x+y); }
    
    Block[] blocks;
    MatrixCollider[] childColliders { get => blocks.Select(b => b.matrixCollider).ToArray(); }
    CollisionMatrix _matrix{ get => CollisionMatrix.instance; }
    Vector2Int _matrixPosition { get => CollisionMatrix.instance.GetMatrixPos(transform); }

    void Awake()
    {
        blocks = GetComponentsInChildren<Block>(true);
    }

    public Vector2Int GetLowestPosition(int xBase)
    {
        int yBaseMin= 0;
        foreach(Block childBlock in blocks)
        {
            Vector2Int relativePosition = childBlock.matrixCollider.matrixPosition - _matrixPosition;
            int x = xBase + relativePosition.x;

            int yMin = 0;
            foreach (Block block in BlockManager.instance.blockList)
            {
                if (block.matrixCollider.matrixPosition.x == x)
                    yMin = Mathf.Max(block.matrixCollider.matrixPosition.y + 1, yMin);
            }
            yBaseMin = Mathf.Max(yBaseMin, yMin - relativePosition.y);
        }
        return new Vector2Int(xBase, yBaseMin);
    }

    public bool IsValidPosition(Vector2Int basePosition)
    {
        foreach (MatrixCollider childCollider in childColliders)
        {
            Vector2Int relativePosition = childCollider.matrixPosition - _matrixPosition;
            Vector2Int positionToCheck = basePosition + relativePosition;

            if (!CollisionMatrix.instance.IsValidPosition(positionToCheck))
                return false;

            List<MatrixCollider> collidersAtPosition = _matrix.GetCollidersAtPosition(positionToCheck).Where(col => !childColliders.Contains(col)).ToList();
            if (collidersAtPosition.Any())
                return false;
        }
        return true;
    }



    public void Move(Vector2Int position)
    {
        transform.position = CollisionMatrix.instance.GetRealWorldPosition(position);
        SynchronizePosition();
    }
    
    public void SynchronizePosition()
    {
        foreach(MatrixCollider childCollider in childColliders)
            childCollider.SynchronizePosition();
    }

    public void Place()
    {
        foreach(Block block in blocks)
            block.Place();
    }
}
