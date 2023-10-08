using DG.Tweening;
using System;
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

    public void MoveTo(TilePawnSlot destination, Action onComplete = null)
    {
        destination.SetObject(this, false);

        this.transform.DOJump(destination.transform.position, 1f, 1, 0.3f).SetEase(Ease.InOutQuint).OnComplete(() => onComplete?.Invoke());
        this.transform.DORotateQuaternion(Quaternion.identity, 0.3f);
        this.transform.DOScale(1f, 0.3f).SetEase(Ease.InOutQuint);
    }
}
