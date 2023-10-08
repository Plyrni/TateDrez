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
    public eChessColor CurrentPlayerColor { get => this.currentPlayerColor; set => SetCurrentPlayer(value); }
    public Player CurrentPlayer { get => this.GetPlayer(this.currentPlayerColor); }

    public GameStateMachine StateMachine;
    public UnityEvent<eChessColor> OnCurrentPlayerChanged;
    public GameBoard gameBoard;

    private List<Player> _listPlayers = new List<Player>();
    private eChessColor currentPlayerColor;
    private int nbBonusTurn = 0;

    private void Awake()
    {
        instance = this;
    }

    public void InitializePlayers()
    {
        PawnContainerManager.Instance.Initialize();
        this.gameBoard.Initialize();

        this._listPlayers.Clear();
        this.AddPlayer(eChessColor.Light);
        this.AddPlayer(eChessColor.Dark);
    }
    public Player GetPlayer(eChessColor color)
    {
        return this._listPlayers[(int)color];
    }

    public void RandomizeFirstPlayer()
    {
        this.SetCurrentPlayer((eChessColor)(Random.Range(0, 100) % 2));
    }
    public void NextPlayer()
    {
        if (this.nbBonusTurn > 0)
        {
            this.nbBonusTurn--;
            return;
        }
        this.SetCurrentPlayer((eChessColor)(((int)currentPlayerColor + 1) % 2));
    }
    public void AddBonusTurn(int nb = 1)
    {
        this.nbBonusTurn+= nb;
        Debug.Log("GET BONUS TURN !! Sheeesh");
    }


    private void SetCurrentPlayer(eChessColor playerColor)
    {
        this.currentPlayerColor = playerColor;
        this.OnCurrentPlayerChanged?.Invoke(this.currentPlayerColor);

        Debug.Log("Current Player = " + this.currentPlayerColor);
    }
    private void AddPlayer(eChessColor playerColor)
    {
        Player newPlayer = new Player();
        newPlayer.Initialize(playerColor);
        this._listPlayers.Add(newPlayer);
    }
}
