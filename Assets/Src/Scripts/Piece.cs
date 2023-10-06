using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] ChessThemeToggler chessThemeToggler;
    private void Awake()
    {

    }

    void Start()
    {

    }


}

[System.Serializable]
class ChessThemeToggler
{
    [SerializeField] GameObject objLight;
    [SerializeField] GameObject objDark;

    public void EnableTheme(eChessColor chessColor)
    {
        this.objLight.SetActive(chessColor == eChessColor.Light);
        this.objDark.SetActive(chessColor == eChessColor.Dark);
    }
}
