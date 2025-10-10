using UnityEngine;
using UnityEditor;

public class Cell : MonoBehaviour
{
    public AnimationCurve moveCurve;
    public float speed;

    float lerpTime = 0;
    Vector3 startPos = Vector3.zero;
    Vector3 targetPos = Vector3.zero;

    float cellSize;
    public float rayDist;
    public Vector3 rayDir;

    public enum MoveStates
    {
        idle,
        moving
    }

    public MoveStates state = MoveStates.idle;

    public bool canMove, canStick;

    public virtual void Start()
    {
        cellSize = EditorSnapSettings.gridSize.x;
        startPos = transform.position;
        targetPos = transform.position;
        rayDist = cellSize;
    }

    public virtual void Update()
    {
        Debug.DrawRay(transform.position, rayDir * rayDist, Color.red);
        if (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Move();
        }
    }

    public Vector3 Move()
    {
        lerpTime += Time.deltaTime * speed;
        float percent = moveCurve.Evaluate(lerpTime);
        Vector3 newPos = Vector3.LerpUnclamped(startPos, targetPos, percent);
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            newPos = FinishMove(targetPos);
        }
        return newPos;
    }

    public virtual void StartMove(Vector3 start, Vector3 end)
    {
        state = MoveStates.moving;
        lerpTime = 0;
        startPos = start;
        targetPos = end;
    }

    public virtual Vector3 FinishMove(Vector3 finalPos)
    {
        state = MoveStates.idle;
        return targetPos;
    }

    public virtual bool CheckMove(int x, int y)
    {
        rayDir = new Vector3(x, 0, y);
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, rayDir, out hit, rayDist))
        {
            Vector3 newPos = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + y);
            StartMove(transform.position, newPos);
            return true;
        }
        else
        {
            if (CheckHit(hit.collider.gameObject, x, y))
            {
                Vector3 newPos = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + y);
                StartMove(transform.position, newPos);
                return true;
            }
        }
        return false;
    }

    bool CheckHit(GameObject hitObj, int x, int y)
    {
        Cell cellScript = hitObj.GetComponent<Cell>();
        if (cellScript != null)
        {
            if (cellScript.canMove)
            {
                if (cellScript.CheckMove(x, y))
                {
                    return true;   
                }
            }
        }
        return false;
    }
}
