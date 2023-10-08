using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChessThemeToggler
{
    public eChessColor CurrentTheme { get => _currentTheme; }

    [SerializeField] GameObject themeLight;
    [SerializeField] GameObject themeDark;
    private eChessColor _currentTheme = eChessColor.NONE;


    public void ToggleTheme(eChessColor chessColor)
    {
        this.themeLight.SetActive(chessColor == eChessColor.Light);
        this.themeDark.SetActive(chessColor == eChessColor.Dark);
        this._currentTheme = chessColor;
    }
}
