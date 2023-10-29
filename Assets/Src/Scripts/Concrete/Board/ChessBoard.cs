using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard : Board2D
{
    public ChessTile2D GetEmptyTile()
    {
        foreach (var item in this._listTiles)
        {
            foreach (var tile in item)
            {
                ChessTile2D chessTile = (tile as ChessTile2D);                
                if (chessTile != null && chessTile.pawnSlot.IsEmpty == true)
                {
                    return chessTile;
                }
            }
        }
        return null;
    }

    public BasePawn GetPawn(Vector2Int cellcoordinates)
    {
        return GetPawn(cellcoordinates.x, cellcoordinates.y);
    }
    public BasePawn GetPawn(int x, int y)
    {
        return this.GetChessTile(x,y)?.Pawn;
    }

    public ChessTile2D GetChessTile(Vector2Int cellcoordinates)
    {
        return this.GetChessTile(cellcoordinates.x, cellcoordinates.y);
    }
    public ChessTile2D GetChessTile(int x, int y)
    {
        return GetTile(x, y) as ChessTile2D;
    }

    public bool IsCellEmpty(int x, int y)
    {
        return this.IsCellEmpty(new Vector2Int(x, y));
    }
    public bool IsCellEmpty(Vector2Int cellCoordinate)
    {
        return GetPawn(cellCoordinate) == null;
    }
}
