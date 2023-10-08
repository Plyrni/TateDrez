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

    public GameStateMachine StateMachine;
    public eChessColor CurrentPlayerColor { get => currentPlayerColor; set => SetCurrentPlayer(value); }
    public UnityEvent<eChessColor> OnCurrentPlayerChanged;
    public GameBoard gameBoard;

    private eChessColor currentPlayerColor;

    private void Awake()
    {
        instance = this;
    }

    public void RandomizeFirstPlayer()
    {
        this.SetCurrentPlayer((eChessColor)(Random.Range(0, 100) % 2));
    }
    public void NextPlayer()
    {
        this.SetCurrentPlayer((eChessColor)(((int)currentPlayerColor + 1) % 2));
    }
    private void SetCurrentPlayer(eChessColor playerColor)
    {
        this.currentPlayerColor = playerColor;
        this.OnCurrentPlayerChanged?.Invoke(this.currentPlayerColor);

        Debug.Log("Current Player = " + this.currentPlayerColor);
    }
}
