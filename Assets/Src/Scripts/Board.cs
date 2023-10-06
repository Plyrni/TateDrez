using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Vector2 boardSize;
    [SerializeField] private Tile tilePrefab;

    private List<List<Tile>> listTiles = new List<List<Tile>>();
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

    private void InitBoardAtStartup()
    {
        /// Since "listTiles" is empty at startup, we make sure there is no Tile left before baking
        foreach (var item in this._grid.GetComponentsInChildren<Tile>())
        {
            Destroy(item.gameObject);
        }

        this.BakeBoard();
    }

    private void BakeBoard()
    {
        ClearBoardTiles();

        Vector3Int currentCell = Vector3Int.zero;
        Vector3 currentCell_WorldCoordinates = Vector3.zero;

        for (int x = 0; x < this.boardSize.x; x++)
        {
            // Update variables
            this.listTiles.Add(new List<Tile>());
            currentCell.x = x;

            for (int y = 0; y < this.boardSize.y; y++)
            {
                // Update variables
                currentCell.y = y;
                currentCell_WorldCoordinates = this._grid.CellToWorld(currentCell);

                Tile newTile = this.SpawnTile(currentCell_WorldCoordinates);
                newTile.cellCoordinates = currentCell;

                // Compute color
                eChessColor tileColor = (x + y) % 2 == 0 ? eChessColor.Light : eChessColor.Dark;
                newTile.SetColor(tileColor);

                // Store tile for future use
                this.listTiles[x].Add(newTile);
            }
        }

        this.CenterGrid();
    }
    private void ClearBoardTiles()
    {
        foreach (var item in listTiles)
        {
            foreach (var tile in item)
            {
                Destroy(tile.gameObject);
            }
            item.Clear();
        }
        this.listTiles.Clear();
    }
    private void CenterGrid()
    {
        this._grid.transform.localPosition = new Vector3(-boardSize.x / 2 * _grid.cellSize.x + _grid.cellSize.x / 2, this._grid.transform.localPosition.y, -boardSize.y / 2 * _grid.cellSize.z + +_grid.cellSize.z / 2);
    }
    private Tile SpawnTile(Vector3 worldCoordinates)
    {
        return Instantiate(this.tilePrefab, worldCoordinates, Quaternion.identity, this._grid.transform);
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Board))]
    public class MyScriptEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Board myScript = (Board)target;
            if (GUILayout.Button("BakeBoard"))
            {
                myScript.BakeBoard();
            }
        }
    }
#endif
}
