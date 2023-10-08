using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TileValidFeedback : MonoBehaviour
{
    [SerializeField] Transform visualAnimated;
    Vector3 baseScale = Vector3.zero;

    private void Awake()
    {
        this.baseScale = this.visualAnimated.lossyScale;
    }
    private void OnEnable()
    {
        this.visualAnimated.DOKill();
        this.Animate();
    }

    private void Animate()
    {
        this.visualAnimated.localScale = baseScale;
        this.visualAnimated.DOScale(0.75f * baseScale, 0.5f).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                this.visualAnimated.DOScale(baseScale, 0.5f).SetEase(Ease.InOutSine).OnComplete(() => Animate());
            });
    }

    private void OnDestroy()
    {
        this.visualAnimated.DOKill();
    }
}
