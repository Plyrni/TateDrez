using UnityEngine;
using UnityEngine.Events;

public class TurnManager
{
    public eChessColor CurrentColorTurn { get; private set; }
    private int nbBonusTurn = 0;
    public UnityEvent<eChessColor> OnCurrentPlayerChanged = new UnityEvent<eChessColor>();

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
        this.SetCurrentPlayer((eChessColor)(((int)this.CurrentColorTurn + 1) % 2));
    }
    public void AddBonusTurn(int nb = 1)
    {
        this.nbBonusTurn += nb;
        Debug.Log("GET BONUS TURN !! Sheeesh");
    }
    private void SetCurrentPlayer(eChessColor playerColor)
    {
        this.CurrentColorTurn = playerColor;
        this.OnCurrentPlayerChanged?.Invoke(this.CurrentColorTurn);
        Debug.Log("Current Player = " + this.CurrentColorTurn);
    }
}
