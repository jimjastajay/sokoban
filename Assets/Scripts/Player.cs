using Unity.Mathematics;
using UnityEngine;

public class Player : Cell
{

    [SerializeField]
    Animator myAnim;

    public override void Update()
    {
        base.Update();

        if (state == MoveStates.idle)
        {
            MoveInput();
        }
    }

    void MoveInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (CheckMove(-1, 0)) transform.rotation = Quaternion.LookRotation(Vector3.left);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (CheckMove(1, 0)) transform.rotation = Quaternion.LookRotation(Vector3.right);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            if (CheckMove(0, 1)) transform.rotation = Quaternion.LookRotation(Vector3.back);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (CheckMove(0, -1)) transform.rotation = Quaternion.LookRotation(Vector3.forward);
        }
    }

    public override void StartMove(Vector3 start, Vector3 end)
    {
        myAnim.SetBool("isMoving", true);
        base.StartMove(start, end);
    }

    public override Vector3 FinishMove(Vector3 finalPos)
    {
        myAnim.SetBool("isMoving", false);
        return base.FinishMove(finalPos);
    }

}
