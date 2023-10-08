using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PawnContainerManager : MonoBehaviour
{
    public static PawnContainerManager Instance => _instance;
    static PawnContainerManager _instance;

    [SerializeField] Transform activePawnSpot;
    [SerializeField] Transform inactivePawnSpot;

    [SerializeField] PawnContainer containerLight;
    [SerializeField] PawnContainer containerDark;

    private void Awake()
    {
        _instance = this;
    }

    public void Initialize()
    {
        this.containerLight.Initialize();
        this.containerDark.Initialize();

        this.containerLight.transform.position = this.inactivePawnSpot.position;
        this.containerDark.transform.position = this.inactivePawnSpot.position;
    }

    public PawnContainer GetContainer(eChessColor chessColor)
    {
        switch (chessColor)
        {
            case eChessColor.Light:
                return containerLight;
            case eChessColor.Dark:
                return containerDark;
        }

        Debug.Log("Cant find container of color " + chessColor);
        return null;
    }

    public void ShowContainer(eChessColor chessColor)
    {
        eChessColor colorToShow = chessColor;
        eChessColor colorToHide = colorToShow == eChessColor.Light ? eChessColor.Dark : eChessColor.Light;

        this.GetContainer(colorToShow).transform.DOMove(this.activePawnSpot.position, 0.4f).SetDelay(0.3f);
        this.GetContainer(colorToHide).transform.DOMove(this.inactivePawnSpot.position, 0.5f);
    }
    public void HideAllContainers()
    {
        this.HideContainer(eChessColor.Light);
        this.HideContainer(eChessColor.Dark);
    }
    private void HideContainer(eChessColor chessColor)
    {
        PawnContainer container = GetContainer(chessColor);
        container.transform.DOKill();
        container.transform.DOMove(this.inactivePawnSpot.position, 0.5f);
    }
}
