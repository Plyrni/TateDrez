using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Board : MonoBehaviour
{
    [HideInInspector]public UnityEvent<Tile> OnTileSpawned;

    [SerializeField] private Vector2 _boardSize;
    [SerializeField] private Tile _tilePrefab;

    private List<List<Tile>> _listTiles = new List<List<Tile>>();
    [SerializeField] private Grid _grid;

    private void Awake()
    {
        if (this._grid == null)
        {
            this._grid = GetComponentInChildren<Grid>();
        }
    }

    void Start()
    {
        this.InitBoardAtStartup();
    }

    public void BakeBoard()
    {
        this.ClearBoardTiles();

        Vector3Int currentCell = Vector3Int.zero;

        for (int x = 0; x < this._boardSize.x; x++)
        {
            // Update variables
            this._listTiles.Add(new List<Tile>());
            currentCell.x = x;

            for (int y = 0; y < this._boardSize.y; y++)
            {
                // Update variables
                currentCell.y = y;

                Tile newTile = this.SpawnTile(currentCell);

                // Store tile for future use
                this._listTiles[x].Add(newTile);
            }
        }

        this.CenterGrid();
    }
    public void ClearBoardTiles()
    {
        foreach (var item in _listTiles)
        {
            foreach (var tile in item)
            {
                Destroy(tile.gameObject);
            }
            item.Clear();
        }
        this._listTiles.Clear();
    }
    public void ClearBoardTiles_Editor()
    {
        /// Since "listTiles" might be empty, we make sure there is no Tile left before baking
        foreach (var item in this._grid.GetComponentsInChildren<Tile>())
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

    private void InitBoardAtStartup()
    {
        /// Rebake board for safety :        
        // We make sure to destroy every existing Tile child
        foreach (var item in this._grid.GetComponentsInChildren<Tile>())
        {
            Destroy(item.gameObject);
        }

        this.BakeBoard();
    }
    private Tile SpawnTile(Vector3Int cellCoordinate)
    {
        Vector3 currentCell_WorldCoordinates = this._grid.CellToWorld(cellCoordinate);
        Tile newTile = Instantiate(this._tilePrefab, currentCell_WorldCoordinates, this._grid.transform.rotation, this._grid.transform);
        newTile.cellCoordinates = cellCoordinate;
        newTile.transform.localScale = this._grid.cellSize;

        this.OnTileSpawned?.Invoke(newTile);
        return newTile;
    }   
    protected void CenterGrid()
    {
        float offsetXV0 = -(_boardSize.x * _grid.cellSize.x + (_boardSize.x - 1) * _grid.cellGap.x) / 2 + _grid.cellSize.x/2;
        float offsetZV0 = -(_boardSize.y * _grid.cellSize.z + (_boardSize.y - 1) * _grid.cellGap.z) / 2 + _grid.cellSize.z/2;

        this._grid.transform.localPosition = new Vector3(offsetXV0, this._grid.transform.localPosition.y, offsetZV0);        
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Board))]
    public class Board_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Board myScript = (Board)target;
            if (GUILayout.Button("BakeBoard (Rebaked at startup)"))
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
}
