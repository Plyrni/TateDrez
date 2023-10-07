using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard_TileController : BaseTileInteractionController
{
    [SerializeField] GameBoard gameBoardOwner;

    public GameBoard_TileController(GameBoard gameBoardOwner)
    {
        this.gameBoardOwner = gameBoardOwner;
    }

    public override void NotifyTouch(ITouchInterractable tile)
    {
        base.NotifyTouch(tile);
    }
}