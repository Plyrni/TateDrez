using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_DynamicPhase : GameState
{
    public override void OnEnter()
    {
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

    }
}