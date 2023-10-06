using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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

//[System.Serializable]
//class ChessThemeToggler
//{
//    [SerializeField] GameObject themeLight;
//    [SerializeField] GameObject themeDark;

//    public void ToggleTheme(eChessColor chessColor)
//    {
//        this.themeLight.SetActive(chessColor == eChessColor.Light);
//        this.themeDark.SetActive(chessColor == eChessColor.Dark);
//    }
//}
