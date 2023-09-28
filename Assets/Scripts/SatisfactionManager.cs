using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatisfactionManager : Singleton<SatisfactionManager>
{
    public GenericGrid<int> satisfactionGrid = new GenericGrid<int>();

    public void ComputeGrid()
    {
        foreach (Block block in BlockManager.instance.blockList)
        {
            foreach (KeyValuePair<Vector2Int, int> pair in block.GetSatisfactionAura())
            {
                Vector2Int pos = pair.Key + block.matrixCollider.matrixPosition;
                satisfactionGrid[pos] += pair.Value;
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
