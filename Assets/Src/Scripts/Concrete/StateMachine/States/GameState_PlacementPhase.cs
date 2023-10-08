using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;


public class GameState_PlacementPhase : GameState
{
    SelectionManager selectionManager;
    PawnContainerManager pawnContainerManager;
    bool _canSelect = false;

    public override void OnEnter()
    {
        base.OnEnter();
        // Init variables
        this.selectionManager = new SelectionManager();
        this.pawnContainerManager = PawnContainerManager.Instance;
        this._canSelect = true;

        // Init state environment
        CameraManager.Instance.SetCamera(eCamType.Game);
        this.gameManager.PlayerManager.InitializePlayers();
        this.gameManager.gameBoard.Initialize();
        UIManager.Instance.SetMenu(eGameState.Placement);
        TileTouchManager.Instance.OnTileTouched.AddListener(OnTileTouched);
        this.gameManager.TurnManager.OnCurrentPlayerChanged.AddListener(this.OnCurrentPlayerChange);
        this.gameManager.TurnManager.RandomizeFirstPlayer();
        
    }

    public override void OnUpdate(float dt)
    {
        base.OnUpdate(dt);
    }
    public override void OnExit()
    {
        base.OnExit();

        // Remove events listeners
        TileTouchManager.Instance.OnTileTouched.RemoveListener(this.OnTileTouched);
        this.gameManager.TurnManager.OnCurrentPlayerChanged.RemoveListener(this.OnCurrentPlayerChange);
        this.pawnContainerManager.HideAllContainers();
        this.gameManager.gameBoard.UnHighlighTiles();
    }

    private void OnTileTouched(ChessTile2D tile)
    {
        if (tile == null) return;
        this.ManageSelection(tile);
    }

    private void ManageSelection(ChessTile2D tile)
    {
        if (this._canSelect == false) { return; }

        // No tile selected yet
        if (this.selectionManager.currentSelection.Count == 0)
        {
            if (tile.Container.Owner.Type == TileContainerOwnerType.PawnContainer)
            {
                if (tile.pawnSlot.IsEmpty == true)
                {
                    Debug.Log("No pawn on this slot");
                    return;
                }
                if (tile.Pawn.ChessColor != this.gameManager.CurrentPlayer.chessColor)
                {
                    Debug.Log("That's not your pawns");
                    return;
                }
                this.SelectFirstPawn(tile);
            }
            else
            {
                Debug.Log("First selection must always be from PawnContainer");
            }
        }
        // Has already a tile selected
        else if (this.selectionManager.currentSelection.Count > 0)
        {
            if (tile.Container.Owner.Type == TileContainerOwnerType.GameBoard)
            {
                if (tile.pawnSlot.IsEmpty == false)
                {
                    Debug.Log("Tile occupied");
                    return;
                }

                this.MovePawnSelectedTo(tile);
                this.selectionManager.Clear();
            }
            else
            {
                this.SelectFirstPawn(tile);
            }
        }
    }
    private void MovePawnSelectedTo(ChessTile2D destination, System.Action onComplete = null)
    {
        ChessTile2D tileSelected = this.selectionManager.GetLastSelected() as ChessTile2D;
        BasePawn pawn = tileSelected.Pawn;
        pawn.SetBaseScale(tileSelected.pawnSlot.transform.lossyScale);
        pawn.EndIdle();
        pawn.MoveTo(destination.pawnSlot, () => this.EndTurn());
        this._canSelect = false;
        this.gameManager.gameBoard.UnHighlighTiles();

    }
    private void SelectFirstPawn(ChessTile2D tile)
    {
        if (tile.IsSelected == true)
        {
            Debug.Log("tile already slected");
            return;
        }

        this.selectionManager.Clear();
        this.selectionManager.Select(tile);
        this.gameManager.gameBoard.UnHighlighTiles();
        this.gameManager.gameBoard.HighLightTiles(this.gameManager.gameBoard.GetEmptyTiles());

        //Debug.Log("Select " + tile);
    }

    private void EndTurn()
    {
        // Check if pawn aligned        
        bool isAligned = false;
        foreach (var pawn in gameManager.CurrentPlayer.Pawns)
        {
            // Only check GameBoard
            if (pawn.slotParent.ChessTileParent.Container.Owner.Type == TileContainerOwnerType.PawnContainer)
            {
                continue;
            }

            isAligned = this.gameManager.gameBoard.IsAligned(pawn.slotParent.ChessTileParent.cellCoordinates.x, pawn.slotParent.ChessTileParent.cellCoordinates.y, 3);
            if (isAligned == true) { break; }
        }

        if (isAligned == true)
        {
            this.gameManager.CurrentPlayer.isWinner = true;
            this.gameManager.StateMachine.SetState(eGameState.Win);
        }
        else
        {
            // End visual feedback
            foreach (var item in this.gameManager.CurrentPlayer.Pawns)
            {
                item.EndIdle();
            }

            this.gameManager.TurnManager.NextPlayer();
            if (this.gameManager.CurrentPlayer.pawnContainer.GetNbPawnsInsideContainerBoard() == 0)
            {
                this.gameManager.StateMachine.SetState(eGameState.Dynamic);
            }
        }
    }
    private void OnCurrentPlayerChange(eChessColor playerColor)
    {
        this._canSelect = true;
        this.pawnContainerManager.ShowContainer(playerColor);
        foreach (var item in this.gameManager.CurrentPlayer.Pawns)
        {
            if (item.slotParent.ChessTileParent.Container.Owner.Type == TileContainerOwnerType.PawnContainer)
            {
                item.StartIdle();
            }
        }
    }
}