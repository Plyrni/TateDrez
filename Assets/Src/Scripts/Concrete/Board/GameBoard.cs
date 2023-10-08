using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameBoard : MonoBehaviour, ITileContainerOwner
{
    [SerializeField] Board _board;

    public ITileContainer TileContainer => this._board;

    private void Awake()
    {
        if (this._board == null)
        {
            this._board = GetComponentInChildren<Board>();
        }
        this._board.Owner = this;
        this._board.OnTileSpawned.AddListener(this.OnSpawnTile);
        this._board.BakeBoard();
    }

    private void OnSpawnTile(ChessTile2D spawnedTile)
    {
        // Compute color
        float moduloTileCoord = (spawnedTile.cellCoordinates.x + spawnedTile.cellCoordinates.y) % 2;
        eChessColor tileColor = moduloTileCoord == 0 ? eChessColor.Light : eChessColor.Dark;
        spawnedTile.SetColorTheme(tileColor);
        //spawnedTile.TouchController = this.tileController;
        spawnedTile.Container= this.TileContainer;
    }

    public bool IsAligned(int row, int col, Board board, int requiredAlignment)
    {
        eChessColor slot = board.GetTile(row, col).ChessColor;
        if (slot == eChessColor.NONE) return false; // Empty slot

        // Directions: Horizontal, Vertical, Diagonal to the right, Diagonal to the left
        int[][] directions = new int[][] { new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 1, 1 }, new int[] { 1, -1 } };

        foreach (var direction in directions)
        {
            int count = 1; // starting with the current slot

            // Check in one direction
            int newRow = row + direction[0];
            int newCol = col + direction[1]; 
            while (this._board.IsInsideBoard(newRow, newCol) && board.GetTile(newRow, newCol).ChessColor == slot)
            {
                count++;
                newRow += direction[0];
                newCol += direction[1];
            }

            // Check in opposite direction (only necessary for the diagonals)
            newRow = row - direction[0];
            newCol = col - direction[1];
            while (this._board.IsInsideBoard(newRow, newCol) && board.GetTile(newRow, newCol).ChessColor == slot)
            {
                count++;
                newRow -= direction[0];
                newCol -= direction[1];
            }

            if (count >= requiredAlignment)
                return true;
        }

        return false;
    }

}