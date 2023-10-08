using System.Collections;
using System.Collections.Generic;
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
    }
    public override void OnUpdate(float dt)
    {
        base.OnUpdate(dt);
    }
    public override void OnExit()
    {
        TileTouchManager.Instance.OnTileTouched.RemoveListener(OnTileTouched);
    }

    private void OnTileTouched(ChessTile2D tile)
    {
        if (tile == null) return;
        this.ManageSelection(tile);
    }

    private void ManageSelection(ChessTile2D tile)
    {
        TileContainerType containerType = this.GetTileContainerType(tile);
        if (containerType == TileContainerType.PawnContainer) { return; }

        // No tile selected yet
        if (this.selectionManager.currentSelection.Count == 0)
        {
            this.TrySelectPawn(tile);
        }
        // Has already a tile selected
        else if (this.selectionManager.currentSelection.Count > 0)
        {
            // Touched a populated tile
            if (tile.Pawn != null)
            {
                // If not you pawn : return
                if (tile.Pawn.ChessColor != this.gameManager.CurrentPlayerColor)
                {
                    Debug.Log("Tile occupied");
                    return;
                }
                // If your pawn : Select
                this.TrySelectPawn(tile);
                return;
            }

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
        if (tile.Pawn.ChessColor != this.gameManager.CurrentPlayerColor)
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
        Debug.Log("Select " + tile);

        return true;
    }
    private void EndTurn()
    {
        // Check if pawn aligned
        bool isAligned = false;
        foreach (var pawn in gameManager.CurrentPlayer.Pawns)
        {
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
            // Check if player pawns can move
            bool canPlayerMove = true; //TODO
            if (canPlayerMove)
            {
                this.gameManager.NextPlayer();
            }
            else
            {
                // Next player should play 2 times
            }
        }
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