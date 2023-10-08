using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnContainer : MonoBehaviour, ITileContainerOwner
{
    [SerializeField] eChessColor chessTeam;
    [SerializeField] Board _board;
    [SerializeField] public List<BasePawn> _listPawnsSpawned;

    public ITileContainer TileContainer => this._board;
    public TileContainerOwnerType Type => TileContainerOwnerType.PawnContainer;

    private void Awake()
    {
        this._board.Owner = this;
        this._board.OnTileSpawned.AddListener(this.OnSpawnTile);
    }

    public void Initialize()
    {
        this._board.BakeBoard();
        this._listPawnsSpawned.Clear();
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
        newPawn.SetBaseScale(newPawn.transform.localScale);
        newPawn.ChessColor = this.chessTeam;
        tile.SetPawn(newPawn);
        this._listPawnsSpawned.Add(newPawn);
        return newPawn;
    }
    private void FillWithPawns()
    {
        this.AddPawn(ePawnType.Knight);
        this.AddPawn(ePawnType.Rook);
        this.AddPawn(ePawnType.Bishop);
    }
    public int GetNbPawnsInsideContainerBoard()
    {
        int nb = 0;
        foreach (var item in _listPawnsSpawned)
        {
            if (item.slotParent.Container.Owner as PawnContainer != null)
            {
                nb++;
            }
        }
        return nb;
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

