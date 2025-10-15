using UnityEngine;
using System.Collections.Generic;

public class Stickable : MonoBehaviour
{


    public List<Block> stuckObj = new List<Block>();
    Block myBlock;

    void Start()
    {
        myBlock = GetComponent<Block>();
    }


    #region Sticky Object Methods
    /// <summary>
    /// Calls when the attached block moves on the gridf
    /// </summary>
    void BlockMoved()
    {
        if (stuckObj.Count > 0) MoveStuckObjs();
    }

    /// <summary>
    /// Moves an sticky blocks attached to this block
    /// </summary>
    private void MoveStuckObjs()
    {
        List<int> _removeList = new List<int>();
        for (int i = 0; i < stuckObj.Count; i++)
        {
            Vector2Int dir = stuckObj[i].gridPos - myBlock.gridPos;
            if (dir != myBlock.moveChange)
            {
                if (!stuckObj[i].CheckMove(myBlock.moveChange.x, myBlock.moveChange.y))
                {
                    _removeList.Add(i);
                    stuckObj[i].gameObject.GetComponent<Sticky>().LeadBlock = null;
                }
            }

        }
        RemoveStuckObjs(_removeList);
    }
    
    /// <summary>
    /// Removes any sticky blocks attached to this block
    /// </summary>
    private void RemoveStuckObjs(List<int> _removeList)
    {
        foreach (int obj in _removeList)
        {
            stuckObj.RemoveAt(obj);
        }
    }

    #endregion
}
