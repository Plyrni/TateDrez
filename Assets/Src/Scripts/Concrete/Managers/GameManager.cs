using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum eChessColor
{
    NONE = -1,
    Light,
    Dark
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get => instance; set => instance = value; }
    static GameManager instance;
    public Player CurrentPlayer { get => this.PlayerManager.GetPlayer(this.TurnManager.CurrentColorTurn); }
    public PlayerManager PlayerManager { get; private set; }
    public TurnManager TurnManager { get; private set; }

    public GameStateMachine StateMachine;
    public GameBoard gameBoard;

    private void Awake()
    {
        instance = this;
        this.TurnManager = new TurnManager();
        this.PlayerManager = new PlayerManager();
    }
}