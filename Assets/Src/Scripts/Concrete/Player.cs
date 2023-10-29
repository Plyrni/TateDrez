using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public eChessColor chessColor;
    public PawnContainer pawnContainer;
    public bool isWinner = false;
    public List<BasePawn> Pawns => pawnContainer.listPawnsSpawned;

    public void InitializeTateDrez(eChessColor color)
    {
        this.chessColor = color;
        this.pawnContainer = PawnContainerManager.Instance.GetContainer(this.chessColor);
        List<ePawnType> listPawnToGenerate = new List<ePawnType> { ePawnType.Knight, ePawnType.Rook, ePawnType.Bishop };
        this.pawnContainer.Initialize(listPawnToGenerate);
    }

    public bool CanPawnsMove()
    {
        int nbPawnsThatCanMove = 0;
        foreach (var currentPawn in Pawns)
        {
            if (currentPawn.movementController.ValidMoves(currentPawn.slotParent.ChessTileParent.cellCoordinates, GameManager.Instance.gameBoard.Board).Count != 0)
            {
                nbPawnsThatCanMove++;
            }
        }
        return nbPawnsThatCanMove != 0;
    }
}
