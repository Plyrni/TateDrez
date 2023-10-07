using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHome : MonoBehaviour
{
    [SerializeField] Button btnStart;

    private void Start()
    {
        this.btnStart.onClick.AddListener(this.OnBtnStartClick);        
    }

    private void OnBtnStartClick()
    {
        GameManager.Instance.StateMachine.SetState(eGameState.Placement);
    }

    private void OnDestroy()
    {
        this.btnStart.onClick.RemoveListener(this.OnBtnStartClick);
    }
}
