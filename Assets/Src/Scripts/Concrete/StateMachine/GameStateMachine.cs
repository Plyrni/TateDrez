using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum eGameState
{
    Menu,
    Placement,
    Dynamic,
    Win
}

public class GameStateMachine : ABaseStateMachine<GameState, eGameState>
{
    [SerializeReference] GameState stateMenu;
    [SerializeReference] GameState statePlacement;
    [SerializeReference] GameState stateDynamic;
    [SerializeReference] GameState stateWin;

    public UnityEvent<eGameState> OnGameStateChanged;

    private void Awake()
    {
        this.SetState(eGameState.Menu);
    }

    protected override GameState GetState(eGameState stateEnum)
    {
        switch (stateEnum)
        {
            case eGameState.Menu:
                return stateMenu;
            case eGameState.Placement:
                return statePlacement;
            case eGameState.Dynamic:
                return stateDynamic;
            case eGameState.Win:
                return stateWin;
        }

        Debug.LogError("state not found in the switch");
        return null;
    }

    public override void SetState(GameState state)
    {
        base.SetState(state);
        this.OnGameStateChanged?.Invoke(this.CurrentStateEnum);
    }
}
