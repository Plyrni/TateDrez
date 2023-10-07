using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class ChessTile2D : MonoBehaviour, ITouchInterractable, ISelectable
{
    [SerializeField] ChessThemeToggler chessThemeToggler;
    [SerializeField] public TileSlot pawnSlot;
    [SerializeField] Transform visual;
    private BoxCollider boxCollider;
    public Vector2Int cellCoordinates { get; set; }
    public ITileContainer Container { get; set; }

    public bool IsSelected { get; set; }

    public ITouchInteraction OnTouchInteraction => this._onTouchInteraction;

    [SerializeReference] ITouchInteraction _onTouchInteraction;

    //public ITouchInteractionController TouchController { get => this._tileController; set => this._tileController = value; }
    //private ITouchInteractionController _tileController;

    private void Awake()
    {
        this.boxCollider = GetComponent<BoxCollider>();
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
    }
    public void OnUnSelect()
    {
        IsSelected = false;
    }
}
