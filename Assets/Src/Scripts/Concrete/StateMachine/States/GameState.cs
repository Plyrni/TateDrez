using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameState : IState
{
    protected GameManager gameManager;
    public virtual void OnEnter()
    {
        Debug.Log("ENTER " + GetType().Name);
        this.gameManager = GameManager.Instance;
    }
    public virtual void OnUpdate(float deltaTime)
    {

    }

    public virtual void OnLateUpdate(float deltaTime)
    {

    }

    public virtual void OnExit()
    {
        Debug.Log("EXIT " + GetType().Name);
    }
}
