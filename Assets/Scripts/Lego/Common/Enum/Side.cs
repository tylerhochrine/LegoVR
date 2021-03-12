using UnityEngine;

public enum Side
{
    NONE, TOP, BOTTOM, LEFT, RIGHT, FRONT, BACK
}

public static class SideMethods
{
    public static Vector3 getDirectionVector3(this Side side)
    {
        switch (side)
        {
            case Side.BACK:
                return Vector3.back;
            case Side.BOTTOM:
                return Vector3.down;
            case Side.FRONT:
                return Vector3.forward;
            case Side.LEFT:
                return Vector3.left;
            case Side.RIGHT:
                return Vector3.right;
            case Side.TOP:
                return Vector3.up;
            default:
                return Vector3.zero;
        }
    }
}