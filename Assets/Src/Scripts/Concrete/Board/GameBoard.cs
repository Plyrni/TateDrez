using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameBoard : MonoBehaviour, ITileContainerOwner
{
    public Board Board { get => _board; }
    [SerializeField] Board _board;

    public ITileContainer TileContainer => this._board;
    public TileContainerOwnerType Type => TileContainerOwnerType.GameBoard;

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
    public void Initialize()
    {
        this._board.BakeBoard();
    }

    private void OnSpawnTile(ChessTile2D spawnedTile)
    {
        // Compute color
        float moduloTileCoord = (spawnedTile.cellCoordinates.x + spawnedTile.cellCoordinates.y) % 2;
        eChessColor tileColor = moduloTileCoord == 0 ? eChessColor.Light : eChessColor.Dark;
        spawnedTile.SetColorTheme(tileColor);
        //spawnedTile.TouchController = this.tileController;
        spawnedTile.Container = this.TileContainer;
    }
    public bool IsAligned(int row, int col, int requiredAlignment)
    {
        ChessTile2D currentTile = this._board.GetTile(row, col);
        if (currentTile.Pawn == null) { return false; }
        eChessColor slot = currentTile.Pawn.ChessColor;
        if (slot == eChessColor.NONE) return false; // Uncolorized Pawn

        // Directions: Horizontal, Vertical, Diagonal to the right, Diagonal to the left
        int[][] directions = new int[][] { new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 1, 1 }, new int[] { 1, -1 } };

        foreach (var direction in directions)
        {
            int count = 1; // starting with the current slot

            // Check in one direction
            int newRow = row + direction[0];
            int newCol = col + direction[1];
            while (this._board.IsInsideBoard(newRow, newCol) && this._board.GetTile(newRow, newCol).Pawn != null && this._board.GetTile(newRow, newCol).Pawn.ChessColor == slot)
            {
                count++;
                newRow += direction[0];
                newCol += direction[1];
            }

            // Check in opposite direction (only necessary for the diagonals)
            newRow = row - direction[0];
            newCol = col - direction[1];
            while (this._board.IsInsideBoard(newRow, newCol) && this._board.GetTile(newRow, newCol).Pawn != null && this._board.GetTile(newRow, newCol).Pawn.ChessColor == slot)
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


    List<Vector2Int> _listLastTilesHighlighted = new List<Vector2Int>();
    public void HighLightTiles(List<Vector2Int> tilesToHighlight, bool shouldHighlight = true)
    {
        foreach (var pos in tilesToHighlight)
        {
            this._board.GetTile(pos).EnableValidFeedback(shouldHighlight);
        }

        if (shouldHighlight == true)
        {
            this._listLastTilesHighlighted = new List<Vector2Int>(tilesToHighlight);
        }
    }
    public void UnHighlighTiles()
    {
        this.HighLightTiles(_listLastTilesHighlighted, false);
    }
    public List<Vector2Int> GetEmptyTiles()
    {
        List<Vector2Int> listEmptyTiles = new List<Vector2Int>();
        Vector2Int currentTile = Vector2Int.zero;
        for (int x = 0; x < this._board.BoardSize.x; x++)
        {
            currentTile.x = x;
            for (int y = 0; y < this._board.BoardSize.y; y++)
            {
                currentTile.y = y;
                if (this._board.IsCellEmpty(currentTile) == true)
                {
                    listEmptyTiles.Add(currentTile);
                }
            }
        }

        return listEmptyTiles;
    }
}