using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseTileInteractionController : ITouchInteractionController
{
    TileSelectionManager tileSelectionManager;

    /// <summary>
    /// Initialize the controller. /!\ Must be called in Start() so you're sure that singletons are initialized
    /// </summary>
    public void Init()
    {
        this.tileSelectionManager = TileSelectionManager.Instance;
    }

    public virtual void NotifyTouch(ITouchInterractable tile)
    {
        Debug.Log(this.GetType().Name + " notified");
    }

    private bool IsTileSelected(ITouchInterractable tile)
    {
        return this.tileSelectionManager.CurrentSelectedTile == tile;
    }
}
