using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Define what happen when you touch the chessTile
/// </summary>
public class DefaultTouchTileInteraction : ITouchInteraction
{
    GameManager gameManager;

    public virtual void Execute(ITouchInterractable interractable)
    {
        ChessTile2D tile = interractable as ChessTile2D;

        this.GetGameManager().StateMachine.NotifyTileTouched(tile);
    }

    private GameManager GetGameManager()
    {
        if (this.gameManager == null) { this.gameManager = GameManager.Instance; }
        return this.gameManager;
    }
}
