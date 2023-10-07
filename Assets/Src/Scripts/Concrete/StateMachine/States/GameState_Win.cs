using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_Win : GameState
{
    public override void OnEnter()
    {
        base.OnEnter();
        UIManager.Instance.SetMenu(eGameState.Win);
    }
    public override void OnUpdate(float dt)
    {
        base.OnUpdate(dt);
    }
    public override void OnExit()
    {

    }
}