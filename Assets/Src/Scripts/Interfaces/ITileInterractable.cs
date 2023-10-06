using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITileInterractable
{
    public ITileInteractionController tileController { get; set; }

    public void OnTouch()
    {
        if (this.tileController == null)
        {
            Debug.LogError("Null TileController");
        }

        this.tileController.OnTouch(this);
    }
}
