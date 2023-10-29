using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopMovement : IPawnMovement
{
    private static readonly List<Vector2Int> possibleDirections = new List<Vector2Int>
    {
        new Vector2Int(1, 1),    // Up-Right
        new Vector2Int(1, -1),   // Down-Right
        new Vector2Int(-1, -1),  // Down-Left
        new Vector2Int(-1, 1)    // Up-Left
    };

    public List<Vector2Int> ValidMoves(Vector2Int currentPosition, ChessBoard board)
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