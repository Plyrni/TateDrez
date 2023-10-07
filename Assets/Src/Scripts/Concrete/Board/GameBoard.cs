using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField] Board _board;
    GameBoard_Controller board_Controller = new GameBoard_Controller();

    private void Awake()
    {
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
        spawnedTile.tileController = board_Controller;
    }
}


public class GameBoard_Controller : ITileInteractionController
{
    public void NotifyTouch(ITileInterractable tile)
    {
        Debug.Log("GameBoard notified");
    }
}


