using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelectionManager : MonoBehaviour
{
    public static TileSelectionManager Instance { get => instance; set => instance = value; }
    private static TileSelectionManager instance;

    public ITileInterractable CurrentSelectedTile { get => this.currentSelectedTile; }
    public ITileInterractable PreviousSelectedTile { get => this.previousSelectedTile; }


    private ITileInterractable currentSelectedTile;
    private ITileInterractable previousSelectedTile;

    private void Awake()
    {
        instance = this;
    }

    public void Select(ITileInterractable tile)
    {
        this.previousSelectedTile = this.currentSelectedTile;
        this.currentSelectedTile = tile;
    }
}
