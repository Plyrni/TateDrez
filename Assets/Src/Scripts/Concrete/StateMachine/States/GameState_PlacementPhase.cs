using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum TileContainerType
{
    NONE = -1,
    GameBoard,
    PawnContainer,
}

public class GameState_PlacementPhase : GameState
{
    SelectionManager selectionManager;
    PawnContainerManager pawnContainerManager;

    public override void OnEnter()
    {
        base.OnEnter();
        // Init variables
        this.selectionManager = new SelectionManager();
        this.pawnContainerManager = PawnContainerManager.Instance;

        // Init state environment
        this.pawnContainerManager.Initialize();
        UIManager.Instance.SetMenu(eGameState.Placement);
        TileTouchManager.Instance.OnTileTouched.AddListener(OnTileTouched);
        this.gameManager.OnCurrentPlayerChanged.AddListener(this.OnCurrentPlayerChange);


        this.gameManager.RandomizeFirstPlayer();
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
        this.gameManager.OnCurrentPlayerChanged.RemoveListener(this.OnCurrentPlayerChange);
    }

    private void OnTileTouched(ChessTile2D tile)
    {
        if (tile == null) return;
        this.ManageSelection(tile);
    }

    private void ManageSelection(ChessTile2D tile)
    {
        TileContainerType containerType = this.GetTileContainerType(tile);        

        // No tile selected yet
        if (this.selectionManager.currentSelection.Count == 0)
        {
            if (containerType == TileContainerType.PawnContainer)
            {
                if (tile.pawnSlot.IsEmpty == true)
                {
                    Debug.Log("No pawn on this slot");
                    return;
                }
                if (tile.ChessColor != this.gameManager.CurrentPlayerColor)
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
            if (containerType == TileContainerType.GameBoard)
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

        tileSelected.Pawn.MoveTo(destination.pawnSlot, () => this.EndTurn());
        
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
        Debug.Log("Select " + tile);
        // Show possible spots
    }

    private void EndTurn()
    {
        // Check if pawn aligned

        this.gameManager.NextPlayer();
    }


    private void OnCurrentPlayerChange(eChessColor playerColor)
    {
        this.pawnContainerManager.ShowContainer(playerColor);
    }
    private TileContainerType GetTileContainerType(ChessTile2D tile)
    {
        if (tile.Container.Owner as GameBoard != null)
        {
            return TileContainerType.GameBoard;
        }
        else if (tile.Container.Owner as PawnContainer != null)
        {
            return TileContainerType.PawnContainer;
        }
        return TileContainerType.NONE;
    }
}