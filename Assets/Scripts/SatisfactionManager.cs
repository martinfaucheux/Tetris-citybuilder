using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SatisfactionManager : Singleton<SatisfactionManager>
{
    private GenericGrid<int> satisfactionGrid = new GenericGrid<int>();
    private BlockContext currentContext { get => BlockContextManager.instance.currentContext; }

    public void ComputeGrid()
    {
        satisfactionGrid = new GenericGrid<int>();
        foreach (Vector2Int blockPosition in currentContext.grid)
        {
            Block block = currentContext.grid[blockPosition];
            foreach (KeyValuePair<Vector2Int, int> pair in block.GetSatisfactionAura())
            {
                Vector2Int targetPosition = blockPosition + pair.Key;
                if (!satisfactionGrid.ContainsKey(targetPosition))
                    satisfactionGrid[targetPosition] = 0;
                satisfactionGrid[targetPosition] += pair.Value;
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
