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

}
