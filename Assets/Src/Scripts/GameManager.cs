using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	Grid _grid;

	private void Awake()
	{
		this._grid = GetComponent<Grid>();
	}

	void Start()
	{
		
	}
}
