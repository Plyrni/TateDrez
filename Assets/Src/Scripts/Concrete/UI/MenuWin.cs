using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuWin : MonoBehaviour
{
    [SerializeField] Button btnBackToMenu;

    private void Awake()
    {
        this.btnBackToMenu.onClick.AddListener(this.OnClickBtnBackToMenu);
    }

    private void OnClickBtnBackToMenu()
    {
        GameManager.Instance.StateMachine.SetState(eGameState.Menu);
    }
}
