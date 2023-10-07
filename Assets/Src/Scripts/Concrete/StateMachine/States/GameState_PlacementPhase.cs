using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_PlacementPhase : GameState
{
    public override void OnEnter()
    {
        base.OnEnter();
        UIManager.Instance.SetMenu(eGameState.Placement);
        GameManager.Instance.RandomizeFirstPlayer();
    }
    public override void OnUpdate(float dt)
    {
        base.OnUpdate(dt);
    }
    public override void OnExit()
    {
        base.OnExit();
    }
}