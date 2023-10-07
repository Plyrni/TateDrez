using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITileInterractable
{
    public ITileInteractionController tileController { get; set; }

    /// <summary>
    /// Called when a tileInterractable gets clicked or touched
    /// </summary>
    public void OnTouch()
    {
        if (this.tileController == null)
        {
            Debug.LogError("Null TileController");
        }
        this.tileController.NotifyTouch(this);
    }
}
