using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameState_DynamicPhase : GameState
{
    SelectionManager selectionManager;


    public override void OnEnter()
    {
        base.OnEnter();
        // Init variables
        this.selectionManager = new SelectionManager();

        UIManager.Instance.SetMenu(eGameState.Dynamic);
        TileTouchManager.Instance.OnTileTouched.AddListener(OnTileTouched);
        this.gameManager.TurnManager.OnCurrentPlayerChanged.AddListener(this.OnCurrentPlayerChange);
        this.HandleCasePlayerBlocked();
        this.EnableCurrentPLayerPawnIdle(true);
    }
    public override void OnUpdate(float dt)
    {
        base.OnUpdate(dt);
    }
    public override void OnExit()
    {
        TileTouchManager.Instance.OnTileTouched.RemoveListener(OnTileTouched);
        this.gameManager.TurnManager.OnCurrentPlayerChanged.RemoveListener(this.OnCurrentPlayerChange);
    }

    private void OnTileTouched(ChessTile2D tile)
    {
        if (tile == null) return;
        this.ManageSelection(tile);
    }

    private void ManageSelection(ChessTile2D tile)
    {
        if (tile.Container.Owner.Type == TileContainerOwnerType.PawnContainer) { return; }

        // No tile selected yet
        if (this.selectionManager.currentSelection.Count == 0)
        {
            this.TrySelectPawn(tile);
        }
        // Has already a tile selected
        else if (this.selectionManager.currentSelection.Count > 0)
        {
            // Try to select a new Pawn
            if (tile.Pawn != null)
            {
                if (tile.Pawn.ChessColor != this.gameManager.CurrentPlayer.chessColor)
                {
                    Debug.Log("Tile occupied");
                    return;
                }
                this.TrySelectPawn(tile);
                return;
            }

            // Try to move the current selected Pawn
            bool canMoveThere = this.possiblesMoves.Contains(tile.cellCoordinates);
            if (canMoveThere == true)
            {
                this.MovePawnSelectedTo(tile);
                this.gameManager.gameBoard.UnHighlighTiles();
                this.selectionManager.Clear();
            }
            else
            {
                Debug.Log("Cant Move here");
            }
        }
    }

    List<Vector2Int> possiblesMoves = new List<Vector2Int>();
    private bool TrySelectPawn(ChessTile2D tile)
    {
        if (tile.pawnSlot.IsEmpty == true)
        {
            Debug.Log("No pawn on this slot");
            return false;
        }
        if (tile.Pawn.ChessColor != this.gameManager.CurrentPlayer.chessColor)
        {
            Debug.Log("That's not your pawns");
            return false;
        }
        if (this.SelectFirstPawn(tile) == false) { return false; }

        this.possiblesMoves = tile.Pawn.movementController.ValidMoves(tile.Pawn.slotParent.ChessTileParent.cellCoordinates, this.gameManager.gameBoard.Board);
        this.gameManager.gameBoard.UnHighlighTiles();
        this.gameManager.gameBoard.HighLightTiles(this.possiblesMoves);

        return true;
    }
    private void MovePawnSelectedTo(ChessTile2D destination, System.Action onComplete = null)
    {
        ChessTile2D tileSelected = this.selectionManager.GetLastSelected() as ChessTile2D;
        tileSelected.Pawn.EndIdle();
        tileSelected.Pawn.MoveTo(destination.pawnSlot, () => this.EndTurn());
    }
    private bool SelectFirstPawn(ChessTile2D tile)
    {
        if (tile.IsSelected == true)
        {
            Debug.Log("Tile already slected");
            return false;
        }

        this.selectionManager.Clear();
        this.possiblesMoves.Clear();
        this.selectionManager.Select(tile);
        //Debug.Log("Select " + tile);

        return true;
    }

    private void OnCurrentPlayerChange(eChessColor playerColor)
    {
        this.EnableCurrentPLayerPawnIdle(true);
    }
    private void EndTurn()
    {
        // Check if pawn aligned
        bool isAligned = false;
        foreach (var pawn in this.gameManager.CurrentPlayer.Pawns)
        {
            isAligned = this.gameManager.gameBoard.IsAligned(pawn.slotParent.ChessTileParent.cellCoordinates.x, pawn.slotParent.ChessTileParent.cellCoordinates.y, 3);
            if (isAligned == true) { break; }
        }
                
        this.EnableCurrentPLayerPawnIdle(false);

        if (isAligned == true)
        {
            this.gameManager.CurrentPlayer.isWinner = true;
            this.gameManager.StateMachine.SetState(eGameState.Win);
        }
        else if (HandleCasePlayerBlocked() == false)
        {
            this.gameManager.TurnManager.NextPlayer();
        }
    }

    private bool HandleCasePlayerBlocked()
    {
        bool canPlayerMove = gameManager.CurrentPlayer.CanPawnsMove();
        if (canPlayerMove == false)
        {
            this.gameManager.TurnManager.NextPlayer();
            this.gameManager.TurnManager.AddBonusTurn();
            return true;
        };
        return false;
    }

    private void EnableCurrentPLayerPawnIdle(bool enable)
    {
        foreach (var item in this.gameManager.CurrentPlayer.Pawns)
        {
            if (enable == true)
            {
                item.StartIdle();
            }
            else
            {
                item.EndIdle();
            }
        }
    }
}