using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eChessColor
{
	Light,
	Dark
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get => instance; set => instance = value; }
	static GameManager instance;


    private void Awake()
	{		
		instance = this;
	}

	void Start()
	{
		
	}
}
