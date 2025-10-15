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

    public Stickable leadBlock;

    public Stickable LeadBlock{
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

    /// <summary>
    /// Checks all orthogonal cells to see if there is a block this block can stick to
    /// </summary>
    private void CheckAdjacent()
    {
        foreach (Vector2Int _dir in touchDirs)
        {
            Cell checkCell = gridManager.gridList[gridPos.x + _dir.x][gridPos.y + _dir.y].GetComponent<Cell>();
            if (checkCell.ContainObj != null && checkCell.ContainObj.TryGetComponent<Stickable>(out Stickable stickTo))
            {
                if (Vector3.Distance(transform.position, stickTo.transform.position) <= 1)
                {
                    LeadBlock = stickTo;
                }
            }
        }
    }

}
