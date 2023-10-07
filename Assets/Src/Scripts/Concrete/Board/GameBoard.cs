using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameBoard : MonoBehaviour, IBoardOwner
{
    [SerializeField] Board _board;
    GameBoard_TileController tileController;

    public Board Board => this._board;

    private void Awake()
    {
        this.tileController = new GameBoard_TileController(this);

        if (this._board == null)
        {
            this._board = GetComponentInChildren<Board>();
        }
        this._board.OnTileSpawned.AddListener(this.OnSpawnTile);
        this._board.BakeBoard();
    }

    private void OnSpawnTile(ChessTile2D spawnedTile)
    {
        // Compute color
        float moduloTileCoord = (spawnedTile.cellCoordinates.x + spawnedTile.cellCoordinates.y) % 2;
        eChessColor tileColor = moduloTileCoord == 0 ? eChessColor.Light : eChessColor.Dark;
        spawnedTile.SetColorTheme(tileColor);
        spawnedTile.touchController = this.tileController;
    }
}



