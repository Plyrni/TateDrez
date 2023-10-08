using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMovement : IPawnMovement
{
    private static readonly List<Vector2Int> possibleOffsets = new List<Vector2Int>
    {
        // Right
        new Vector2Int(1, 0),
        new Vector2Int(2, 0),
        new Vector2Int(3, 0),
        new Vector2Int(3, 1),
        new Vector2Int(3, -1),
        
        // Left
        new Vector2Int(-1, 0),
        new Vector2Int(-2, 0),
        new Vector2Int(-3, 0),
        new Vector2Int(-3, 1),
        new Vector2Int(-3, -1),

        // Up
        new Vector2Int(0, 1),
        new Vector2Int(0, 2),
        new Vector2Int(0, 3),
        new Vector2Int(1, 3),
        new Vector2Int(-1, 3),

        //Down
        new Vector2Int(0, -1),
        new Vector2Int(0, -2),
        new Vector2Int(0, -3),
        new Vector2Int(1, -3),
        new Vector2Int(-1, -3),
    }; 

    public List<Vector2Int> ValidMoves(Vector2Int currentPosition, Board board)
    {
        List<Vector2Int> validMoves = new List<Vector2Int>();

        // Check for every offset if the tile is empty
        // Could be more optimized but its chill for now
        foreach (var offset in possibleOffsets)
        {
            Vector2Int newPosition = currentPosition + offset;

            if (board.IsInsideBoard(newPosition) && board.IsCellEmpty(newPosition))
            {
                validMoves.Add(newPosition);
            }
        }

        return validMoves;
    }
}
