using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;



public class Tile : MonoBehaviour
{
	[SerializeField] Color _colorLight;
	[SerializeField] Color _colorDark;
	[HideInInspector] public Vector3 cellCoordinates;
	[SerializeField] MeshRenderer meshRenderer;

	public virtual void SetColor(eChessColor color)
    {
		Color newColor = color == eChessColor.Light ? _colorLight : _colorDark;
		this.meshRenderer.material.color = newColor;
    }
}
