using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookMovement : IPawnMovement
{
    private static readonly List<Vector2Int> possibleDirections = new List<Vector2Int>
    {
        new Vector2Int(0, 1),    // Up
        new Vector2Int(1, 0),    // Right
        new Vector2Int(0, -1),   // Down
        new Vector2Int(-1, 0)    // Left
    };

    public List<Vector2Int> ValidMoves(Vector2Int currentPosition, Board board)
    {
        List<Vector2Int> validMoves = new List<Vector2Int>();

        foreach (var direction in possibleDirections)
        {
            Vector2Int newPosition = currentPosition + direction;

            while (board.IsInsideBoard(newPosition) && board.IsCellEmpty(newPosition))
            {
                validMoves.Add(newPosition);
                newPosition += direction;
            }
        }

        return validMoves;
    }
}
