using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class TileSelectionManager : MonoBehaviour
{
    public static TileSelectionManager Instance { get => instance; set => instance = value; }
    private static TileSelectionManager instance;

    public ITouchInterractable CurrentSelectedTile { get => this.currentSelectedTile; }
    public ITouchInterractable PreviousSelectedTile { get => this.previousSelectedTile; }


    private ITouchInterractable currentSelectedTile;
    private ITouchInterractable previousSelectedTile;

    private void Awake()
    {
        instance = this;
    }

    public void Select(ITouchInterractable tile)
    {
        this.previousSelectedTile = this.currentSelectedTile;
        this.currentSelectedTile = tile;
    }
}
