using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class ChessTile2D : MonoBehaviour, ITileInterractable
{
	[SerializeField] ChessThemeToggler chessThemeToggler;
	[SerializeField] public TileSlot pawnSlot;
	[SerializeField] Transform visual;

	public Vector2Int cellCoordinates { get; set; }
    public ITileInteractionController tileController { get => this._tileController; set => this._tileController = value; }
    private ITileInteractionController _tileController;

    public void SetVisualScale(Vector3 newScale)
    {
		this.visual.transform.localScale = newScale;
	}

	public virtual void SetColorTheme(eChessColor colorTheme)
    {
		this.chessThemeToggler.ToggleTheme(colorTheme);
    }
}
