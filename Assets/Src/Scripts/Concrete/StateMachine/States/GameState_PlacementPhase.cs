using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_PlacementPhase : GameState
{
    public override void OnEnter()
    {
        base.OnEnter();
        UIManager.Instance.SetMenu(eGameState.Placement);
        this.gameManager.RandomizeFirstPlayer();
        TileTouchManager.Instance.OnTileTouched.AddListener(OnTileTouched);
    }

    public override void OnUpdate(float dt)
    {
        base.OnUpdate(dt);
    }
    public override void OnExit()
    {
        base.OnExit();
        TileTouchManager.Instance.OnTileTouched.RemoveListener(OnTileTouched);
    }

    private void OnTileTouched(ChessTile2D tile)
    {
        Debug.Log("You touched a tile in state " + GetType().Name);
    }
}