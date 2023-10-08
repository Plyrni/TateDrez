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
    public TilePawnSlot slotParent;
    public eChessColor ChessColor { get => this.chessThemeToggler.CurrentTheme; set => this.SetChessColor(value); }
    [SerializeField] ChessThemeToggler chessThemeToggler;
    [SerializeReference] public IPawnMovement movementController;
    Vector3 _baseScale = Vector3.one;

    private void Start()
    {
        this._baseScale = this.transform.lossyScale;
    }

    public void SetChessColor(eChessColor color)
    {
        this.chessThemeToggler.ToggleTheme(color);
        this.gameObject.name += "_" + color;
    }
    public void MoveTo(TilePawnSlot destination, Action onComplete = null)
    {
        this.slotParent?.UnsetObject();
        destination.SetObject(this, false);

        this.transform.DOJump(destination.transform.position, 1f, 1, 0.3f).SetEase(Ease.InOutQuint).OnComplete(() => onComplete?.Invoke());
        this.transform.DORotateQuaternion(Quaternion.identity, 0.3f);
        this._baseScale = Vector3.one;
        this.transform.DOScale(1f, 0.3f).SetEase(Ease.InOutQuint);
    }
    public void SetBaseScale(Vector3 scale)
    {
        this._baseScale = scale;
    }
    public void StartIdle()
    {
        this.transform.DOScaleY(_baseScale.y * 0.75f, 0.5f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            this.transform.DOScaleY(_baseScale.y, 0.5f).SetEase(Ease.InOutSine).OnComplete(() => StartIdle());
        });

        this.transform.DOScaleX(_baseScale.x * 1.1f, 0.5f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            this.transform.DOScaleX(_baseScale.x, 0.5f).SetEase(Ease.InOutSine);
        });
        this.transform.DOScaleZ(_baseScale.z * 1.1f, 0.5f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            this.transform.DOScaleZ(_baseScale.z, 0.5f).SetEase(Ease.InOutSine);
        });
    }
    public void EndIdle()
    {
        this.transform.DOKill();
        this.transform.DOScaleY(_baseScale.y, 0.5f).SetEase(Ease.InOutSine);
        this.transform.DOScaleX(_baseScale.x, 0.5f).SetEase(Ease.InOutSine);
        this.transform.DOScaleZ(_baseScale.z, 0.5f).SetEase(Ease.InOutSine);
    }

}
