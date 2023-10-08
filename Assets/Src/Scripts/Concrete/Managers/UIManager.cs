using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get => instance; set => instance = value; }
    static UIManager instance;

    [SerializeField] GameObject menuHome;
    [SerializeField] GameObject menuGame;
    [SerializeField] GameObject menuWin;

    void Awake()
    {
        instance = this;
    }

    public void SetMenu(eGameState gameState)
    {
        this.menuHome.SetActive(gameState == eGameState.Menu);
        this.menuGame.SetActive(gameState == eGameState.Placement || gameState == eGameState.Dynamic);
        this.menuWin.SetActive(gameState == eGameState.Win);
    }
}
