using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extensions
{
    public static Vector2Int ToVec2_XZ(this Vector3Int pos)
    {
        return new Vector2Int(pos.x,pos.z);
    }
    public static Vector2Int ToVec2_XY(this Vector3Int pos)
    {
        return new Vector2Int(pos.x, pos.y);
    }
}
