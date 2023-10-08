using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ColorTurnAnnouncer : MonoBehaviour
{
    [SerializeField] Image imgBorders;
    [SerializeField] TextMeshProUGUI textColor;
    [SerializeField] RectTransform spotTextDestination;
    [SerializeField] RectTransform spotTextDefault;

    [SerializeField] Color colorLight;
    [SerializeField] Color colorDark;

    private void OnEnable()
    {
        GameManager GMref= GameManager.Instance;
        if (GMref != null)
        {
            GMref.TurnManager.OnCurrentPlayerChanged.AddListener(this.OnPlayerTurnChanged);        
        }
    }


    private void OnPlayerTurnChanged(eChessColor turn)
    {
        Color turnColor = GetColor(turn);
        this.imgBorders.color = turnColor;
        this.textColor.text = turn.ToString();
        this.textColor.color = turnColor;
        this.textColor.fontSharedMaterial.SetColor("_UnderlayColor", this.GetColor((eChessColor)((int)(turn + 1) % 2))); //Use the next turn color
        this.AnimateText();
    }

    private void AnimateText()
    {
        this.textColor.rectTransform.DOKill(); // Prevent animation conflicts

        this.textColor.rectTransform.DOScale(2f, 0.2f);
        this.textColor.rectTransform.DOAnchorPos(this.spotTextDestination.anchoredPosition, 0.2f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            this.textColor.rectTransform.DOPunchRotation(Vector3.forward * 30f,0.5f,10).OnComplete(() =>
            {
                this.textColor.rectTransform.DOScale(1f, 0.2f);
                this.textColor.rectTransform.DOAnchorPos(this.spotTextDefault.anchoredPosition, 0.2f).SetEase(Ease.InOutSine);
            });
        });
    }

    private Color GetColor(eChessColor color)
    {
        switch (color)
        {
            case eChessColor.NONE:
                break;
            case eChessColor.Light:
                return colorLight;
            case eChessColor.Dark:
                return colorDark;
        }

        Debug.Log("Cant find color " + color);
        return colorLight;
    }

    private void OnDisable()
    {
        GameManager.Instance?.TurnManager.OnCurrentPlayerChanged.RemoveListener(this.OnPlayerTurnChanged);
    }
    private void OnDestroy()
    {
        GameManager.Instance.TurnManager.OnCurrentPlayerChanged.RemoveListener(this.OnPlayerTurnChanged);
    }
}
