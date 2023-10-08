using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player
{
    public eChessColor chessColor;
    public PawnContainer pawnContainer;
    public bool isWinner = false;
    public List<BasePawn> Pawns => pawnContainer._listPawnsSpawned;    

    public void Initialize(eChessColor color)
    {
        this.chessColor = color;
        this.pawnContainer = PawnContainerManager.Instance.GetContainer(this.chessColor);
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
