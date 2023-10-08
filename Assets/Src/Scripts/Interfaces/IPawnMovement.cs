using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPawnMovement
{
    List<Vector2Int> ValidMoves(Vector2Int currentPosition, Board board);
}
