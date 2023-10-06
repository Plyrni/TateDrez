using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChessThemeToggler
{
    [SerializeField] GameObject themeLight;
    [SerializeField] GameObject themeDark;

    public void ToggleTheme(eChessColor chessColor)
    {
        this.themeLight.SetActive(chessColor == eChessColor.Light);
        this.themeDark.SetActive(chessColor == eChessColor.Dark);
    }
}
