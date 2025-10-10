using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Sticky : Cell
{

    Vector3[] rayDirs = {
        new Vector3(1, 0, 0),
        new Vector3(-1, 0, 0),
        new Vector3(0, 0, 1),
        new Vector3(0, 0, -1)
    };

    Cell leadingCell = null;

    public override void Start()
    {
        base.Start();
        CheckAdjacent();
    }

    public override void Update()
    {
        base.Update();
        if(state == MoveStates.idle)
        {
            if(leadingCell != null)
            {
                CheckFollow();
            }
            else
            {
                CheckAdjacent();
            }
        }
    }

    void CheckAdjacent()
    {
        RaycastHit hit;
        
        foreach (Vector3 dir in rayDirs)
        {
            if (Physics.Raycast(transform.position, dir, out hit, rayDist))
            {
                Cell hitCell = hit.collider.gameObject.GetComponent<Cell>();
                if (hitCell != null)
                {
                    if (hitCell.canStick && hitCell.state == MoveStates.idle)
                    {
                        Debug.Log("hit");
                        leadingCell = hit.collider.gameObject.GetComponent<Cell>();
                        speed = leadingCell.speed;
                    }
                }
            }
        }
    }

    void CheckFollow()
    {
        if (leadingCell.state == MoveStates.moving)
        {
            if (!CheckMove((int)leadingCell.rayDir.x, (int)leadingCell.rayDir.z))
            {
                leadingCell = null;
            }
        }
    }

    public override bool CheckMove(int x, int y)
    {
        rayDir = new Vector3(x, 0, y);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, rayDir, out hit, rayDist))
        {
            if(hit.collider.gameObject == leadingCell.gameObject && leadingCell.gameObject.CompareTag("Player"))
            {
                Vector3 newPos = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + y);
                StartMove(transform.position, newPos);
                return true;
            }
        }
        return base.CheckMove(x, y);
    }
}
