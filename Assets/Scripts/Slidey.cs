using UnityEngine;

public class Slidey : Cell
{

    public override Vector3 FinishMove(Vector3 finalPos)
    {
        Slide((int)rayDir.x, (int)rayDir.z);
        return transform.position;
    }

    void Slide(int x, int y)
    {
        CheckMove(x, y);
    }

}
