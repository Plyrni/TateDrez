using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class ChessTile2D : MonoBehaviour, ITouchInterractable, ISelectable, ITileContainerElement
{
    [SerializeField] public TilePawnSlot pawnSlot;
    [SerializeField] ChessThemeToggler chessThemeToggler;
    [SerializeField] Transform visual;
    [SerializeField] Transform visualValidFeedback;
    [SerializeReference] ITouchInteraction _onTouchInteraction;
    private BoxCollider boxCollider;

    public eChessColor ChessColor => chessThemeToggler.CurrentTheme;
    public Vector2Int cellCoordinates { get; set; }
    public ITileContainer Container { get; set; }
    public ITouchInteraction OnTouchInteraction => this._onTouchInteraction;
    public bool IsSelected { get; set; }
    public BasePawn Pawn => this.pawnSlot.slotedObject;

    private void Awake()
    {
        this.boxCollider = GetComponent<BoxCollider>();
        this.pawnSlot.ChessTileParent = this;
    }
    private void Start()
    {
        this.pawnSlot.Container = Container;
    }

    public void SetVisualScale(Vector3 newScale)
    {
        this.visual.transform.localScale = newScale;

        BoxCollider boxCollider = GetComponent<BoxCollider>();
        Vector3 size = boxCollider.size;
        size.x *= newScale.x;
        size.y *= newScale.y;
        size.z *= newScale.z;
        boxCollider.size = size;
        Vector3 center = boxCollider.center;
        center.x *= newScale.x;
        center.y *= newScale.y;
        center.z *= newScale.z;
        boxCollider.center = center;
    }
    public virtual void SetColorTheme(eChessColor colorTheme)
    {
        this.chessThemeToggler.ToggleTheme(colorTheme);
    }
    public void SetPawn(BasePawn pawn, bool teleport = true)
    {
        this.pawnSlot.SetObject(pawn, teleport);
    }
    public void EnableValidFeedback(bool enable)
    {
        this.visualValidFeedback.gameObject.SetActive(enable);
    }

    public void OnTouch()
    {
        this._onTouchInteraction.Execute(this);
    }
    public void EnableInterraction(bool enable)
    {
        this.boxCollider.enabled = enable;
    }

    public void OnSelect()
    {
        IsSelected = true;
        this.visual.DOLocalMoveY(0.3f * this.visual.transform.localScale.y, 0.2f);
    }
    public void OnUnSelect()
    {
        IsSelected = false;
        this.visual.DOKill();
        this.visual.DOLocalMoveY(0f, 0.2f);
    }
}
