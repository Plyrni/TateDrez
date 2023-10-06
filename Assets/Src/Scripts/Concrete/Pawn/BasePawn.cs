using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum ePawnType
{
    Knight,
    Rook,
    Bishop,
}

public class BasePawn : MonoBehaviour
{
    [SerializeField] public ChessThemeToggler chessThemeToggler;
}
