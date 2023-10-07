using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Board : MonoBehaviour, ITileContainer
{
    public ITileContainerOwner Owner { get; set; }
    public Grid Grid { get => _grid; }

    [HideInInspector] public UnityEvent<ChessTile2D> OnTileSpawned;

    [SerializeField] private Vector2 _boardSize;
    [SerializeField] private ChessTile2D _tilePrefab;
    [SerializeField] private Grid _grid;
    private List<List<ChessTile2D>> _listTiles = new List<List<ChessTile2D>>();

    private void Awake()
    {
        if (this._grid == null)
        {
            this._grid = GetComponentInChildren<Grid>();
        }
    }

    public ChessTile2D GetEmptyTile()
    {
        foreach (var item in this._listTiles)
        {
            foreach (var tile in item)
            {
                if (tile.pawnSlot.IsEmpty == true)
                {
                    return tile;
                }
            }
        }
        return null;
    }
    public ChessTile2D GetTile(int x, int y)
    {
        if (this._listTiles.Count == 0 ||
               x >= this._listTiles.Count ||
               y >= this._listTiles[0].Count)
        {
            Debug.LogWarning("OutOfBound : " + x + ":" + y);
            return null;
        }

        return this._listTiles[x][y];
    }
    public ChessTile2D GetTile(Vector2Int coord)
    {
        return this.GetTile(coord.x, coord.y);
    }

    public void BakeBoard()
    {
        this.ClearBoardTiles();

        Vector3Int currentCell = Vector3Int.zero;

        for (int x = 0; x < this._boardSize.x; x++)
        {
            // Update variables
            this._listTiles.Add(new List<ChessTile2D>());
            currentCell.x = x;

            for (int y = 0; y < this._boardSize.y; y++)
            {
                // Update variables
                currentCell.y = y;

                ChessTile2D newTile = this.SpawnTile(currentCell);

                // Store tile for future use
                this._listTiles[x].Add(newTile);
            }
        }

        this.CenterGrid();
    }

    /// <summary>Delete all tiles and clear all lists</summary>
    public void ClearBoardTiles()
    {
        if (this._listTiles.Count > 0)
        {
            foreach (var item in _listTiles)
            {
                foreach (var tile in item)
                {
                    Destroy(tile.gameObject);
                }
                item.Clear();
            }
        }
        // We make sure to destroy every existing Tile child. Ex: At startup list is empty even if we've baked it in editor
        else
        {
            foreach (var item in this._grid.GetComponentsInChildren<ChessTile2D>())
            {
                Destroy(item.gameObject);
            }
        }
        this._listTiles.Clear();
    }
    public void ClearBoardTiles_Editor()
    {
        /// Since "listTiles" might be empty, we make sure there is no Tile left before baking
        foreach (var item in this._grid.GetComponentsInChildren<ChessTile2D>())
        {
            DestroyImmediate(item.gameObject);
        }

        /// Clear list in case it isn't empty
        foreach (var item in _listTiles)
        {
            item.Clear();
        }
        this._listTiles.Clear();
    }

    private void CenterGrid()
    {
        float offsetXV0 = -(_boardSize.x * _grid.cellSize.x + (_boardSize.x - 1) * _grid.cellGap.x) / 2 + _grid.cellSize.x / 2;
        float offsetZV0 = -(_boardSize.y * _grid.cellSize.z + (_boardSize.y - 1) * _grid.cellGap.z) / 2 + _grid.cellSize.z / 2;

        this._grid.transform.localPosition = new Vector3(offsetXV0, this._grid.transform.localPosition.y, offsetZV0);
    }
    private ChessTile2D SpawnTile(Vector3Int cellCoordinate)
    {
        Vector3 currentCell_WorldCoordinates = this._grid.CellToWorld(cellCoordinate);
        ChessTile2D newTile = Instantiate(this._tilePrefab, currentCell_WorldCoordinates, this._grid.transform.rotation, this._grid.transform);
        newTile.cellCoordinates = cellCoordinate.ToVec2_XY();
        newTile.SetVisualScale(this._grid.cellSize);

        this.OnTileSpawned?.Invoke(newTile);
        return newTile;
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(Board))]
public class Board_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Board myScript = (Board)target;
        if (GUILayout.Button("BakeBoard"))
        {
            if (Application.isPlaying == false)
            {
                myScript.ClearBoardTiles_Editor();
            }
            myScript.BakeBoard();
        }
    }
}
#endif
