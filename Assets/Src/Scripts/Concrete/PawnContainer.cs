using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnContainer : MonoBehaviour
{
    [SerializeField] eChessColor chessTeam;
    [SerializeField] Board _board;
    private PawnContainer_Controller tileController = new PawnContainer_Controller();

    private void Awake()
    {
        this._board.OnTileSpawned.AddListener(this.OnSpawnTile);
        this._board.BakeBoard();
    }

    private void Start()
    {
        this.FillWithPawn();
    }

    private void FillWithPawn()
    {
        this.AddPawn(ePawnType.Knight);
        this.AddPawn(ePawnType.Rook);
        this.AddPawn(ePawnType.Bishop);
    }
    public void AddPawn(ePawnType pawnType)
    {
        ChessTile2D tile = this._board.GetEmptyTile();
        if (tile == null)
        {
            Debug.LogWarning("Cant find empty Slot");
            return;
        }

        BasePawn newPawn = Instantiate(PawnInstancier.Instance.GetPrefab(pawnType), null);
        newPawn.transform.localScale = this._board.Grid.cellSize.x * Vector3.one;
        newPawn.chessThemeToggler.ToggleTheme(this.chessTeam);
        tile.pawnSlot.SetObject(newPawn.gameObject);
        tile.tileController = this.tileController;
    }

    public void OnSpawnTile(ChessTile2D chessTile)
    {
        chessTile.SetColorTheme(chessTeam);
    }

    private void OnDestroy()
    {
        this._board.OnTileSpawned.RemoveListener(this.OnSpawnTile);
    }
}

public class PawnContainer_Controller : ITileInteractionController
{

}
