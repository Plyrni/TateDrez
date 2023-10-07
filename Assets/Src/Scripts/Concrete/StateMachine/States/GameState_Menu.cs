using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameState_Menu : GameState
{
    public override void OnEnter()
    {
        base.OnEnter();
        UIManager.Instance.SetMenu(eGameState.Menu);
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
