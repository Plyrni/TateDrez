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
    public virtual void OnUpdate(float ddeltaTime)
    {

    }
    public virtual void OnExit()
    {

    }

    public void OnChessTileTouched(ChessTile2D tile)
    {
        //Debug.Log(tile + " notified having been touched. Owner = " + tile.Container.Owner);
    }
}
