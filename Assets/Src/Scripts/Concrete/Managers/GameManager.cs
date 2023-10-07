using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eChessColor
{
	Light,
	Dark
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get => instance; set => instance = value; }
	static GameManager instance;

	public GameStateMachine StateMachine;

	eChessColor currentPlayerColor;

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
		Debug.Log("Current Player = " + this.currentPlayerColor);
    }
}
