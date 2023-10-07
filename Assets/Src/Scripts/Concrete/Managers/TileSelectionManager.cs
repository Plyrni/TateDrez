using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class TileSelectionManager : MonoBehaviour
{
    public static TileSelectionManager Instance { get => instance; set => instance = value; }
    private static TileSelectionManager instance;

    public ChessTile2D CurrentSelectedTile { get => this.currentSelectedTile; }
    public ChessTile2D PreviousSelectedTile { get => this.previousSelectedTile; }


    private ChessTile2D currentSelectedTile;
    private ChessTile2D previousSelectedTile;

    private void Awake()
    {
        instance = this;
    }

    public void Select(ChessTile2D selectable)
    {
        this.previousSelectedTile = this.currentSelectedTile;
        this.currentSelectedTile = selectable;
    }
}
