using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Tile : MonoBehaviour
{
	[SerializeField] MeshRenderer meshRenderer;
	[SerializeField] Color _colorLight;
	[SerializeField] Color _colorDark;

	public virtual void SetColor(eChessColor color)
    {
		Color newColor = color == eChessColor.Light ? _colorLight : _colorDark;
		this.meshRenderer.material.color = newColor;
    }
}
