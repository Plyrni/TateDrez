using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnContainer : MonoBehaviour, ITileContainerOwner
{
    [SerializeField] eChessColor chessTeam;
    [SerializeField] Board _board;
    [SerializeField] public List<BasePawn> _listPawns;

    public ITileContainer TileContainer => this._board;
    private void Awake()
    {
        this._board.Owner = this;
        this._board.OnTileSpawned.AddListener(this.OnSpawnTile);
    }

    public void Initialize()
    {
        this._board.BakeBoard();
        this._listPawns.Clear();
        this.FillWithPawns();
    }
    public BasePawn AddPawn(ePawnType pawnType)
    {
        ChessTile2D tile = this._board.GetEmptyTile();
        if (tile == null)
        {
            Debug.LogWarning("Cant find empty Slot");
            return null;
        }

        BasePawn newPawn = Instantiate(PawnInstancier.Instance.GetPrefab(pawnType), null);
        newPawn.transform.localScale = this._board.Grid.cellSize.x * Vector3.one;
        newPawn.chessThemeToggler.ToggleTheme(this.chessTeam);
        tile.SetPawn(newPawn);
        this._listPawns.Add(newPawn);
        return newPawn;
    }
    private void FillWithPawns()
    {
        this.AddPawn(ePawnType.Knight);
        this.AddPawn(ePawnType.Rook);
        this.AddPawn(ePawnType.Bishop);
    }

    public void OnSpawnTile(ChessTile2D chessTile)
    {
        chessTile.SetColorTheme(this.chessTeam);
        chessTile.Container = TileContainer;
    }

    private void OnDestroy()
    {
        this._board.OnTileSpawned.RemoveListener(this.OnSpawnTile);
    }
}

