using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Sticky : Block
{
    private Vector2Int[] touchDirs = {
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(0, -1)
    };

    private Block leadBlock;

    public Block LeadBlock{
        get => leadBlock;
        set
        {
            leadBlock = null;
            if(value != null) value.stuckObj.Add(this);
            leadBlock = value;
        }
    }

    protected override void Start()
    {
        base.Start();
        CheckAdjacent();
    }

    private void Update()
    {
        if(State == MoveStates.idle && LeadBlock == null)
        {
            CheckAdjacent();
        }
    }

    private void CheckAdjacent()
    {
        foreach (Vector2Int _dir in touchDirs)
        {
            GameObject stickTo = GridManager.gridList[gridPos.x + _dir.x][gridPos.y + _dir.y].GetComponent<Cell>().ContainObj;
            if (stickTo != null)
            {
                Block blockScript = stickTo.GetComponent<Block>();
                if (blockScript.CheckLerpDist(stickTo.transform.position, 0.2f))
                {
                    LeadBlock = blockScript;
                }
            }
        }
    }

}
