using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    private List<Player> _listPlayers = new List<Player>();

    public void InitializePlayers()
    {
        this._listPlayers.Clear();
        this.AddPlayer(eChessColor.Light);
        this.AddPlayer(eChessColor.Dark);
    }
    public Player GetPlayer(eChessColor color)
    {
        return this._listPlayers[(int)color];
    }
    private void AddPlayer(eChessColor playerColor)
    {
        Player newPlayer = new Player();
        newPlayer.Initialize(playerColor);
        this._listPlayers.Add(newPlayer);
    }
}
