using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_DynamicPhase : GameState
{
    public override void OnEnter()
    {
        UIManager.Instance.SetMenu(eGameState.Dynamic);
    }
    public override void OnUpdate(float dt)
    {
        base.OnUpdate(dt);
    }
    public override void OnExit()
    {

    }
}