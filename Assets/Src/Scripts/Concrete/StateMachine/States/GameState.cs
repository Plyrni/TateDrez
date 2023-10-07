using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameState : IState
{
    public virtual void OnEnter()
    {
    }
    public virtual void OnUpdate(float ddeltaTime)
    {

    }
    public virtual void OnExit()
    {

    }
}
