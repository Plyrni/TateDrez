using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;



public class Tile : MonoBehaviour
{
	[SerializeField] ChessThemeToggler chessThemeToggler;
	public Vector3 cellCoordinates;
	public virtual void SetColorTheme(eChessColor colorTheme)
    {
		this.chessThemeToggler.ToggleTheme(colorTheme);
    }
}
