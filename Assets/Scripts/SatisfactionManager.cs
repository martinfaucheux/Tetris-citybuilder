using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatisfactionManager : Singleton<SatisfactionManager>
{
    public GenericGrid<int> satisfactionGrid = new GenericGrid<int>();

    public void ComputeGrid()
    {
        satisfactionGrid = new GenericGrid<int>();
        foreach (Block block in BlockManager.instance.blockList)
        {
            foreach (KeyValuePair<Vector2Int, int> pair in block.GetSatisfactionAura())
            {
                Vector2Int matrixPosition = pair.Key + block.matrixCollider.matrixPosition;
                if (!satisfactionGrid.ContainsKey(matrixPosition))
                    satisfactionGrid[matrixPosition] = 0;
                satisfactionGrid[matrixPosition] += pair.Value;
            }
        }
    }

    public int GetSatisfaction(Vector2Int pos)
    {
        if (satisfactionGrid.ContainsKey(pos))
            return satisfactionGrid[pos];
        return 0;
    }
}
