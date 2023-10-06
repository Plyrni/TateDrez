using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class ChessTile2D : MonoBehaviour
{
	[SerializeField] ChessThemeToggler chessThemeToggler;
	[SerializeField] public TileSlot pawnSlot;
	[SerializeField] Transform visual;

	public Vector2Int cellCoordinates { get; set; }

	public void SetVisualScale(Vector3 newScale)
    {
		this.visual.transform.localScale = newScale;
	}
	public virtual void SetColorTheme(eChessColor colorTheme)
    {
		this.chessThemeToggler.ToggleTheme(colorTheme);
    }

}
