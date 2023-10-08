using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePawnSlot : ASlot<BasePawn>, ITileContainerElement
{
    public ChessTile2D ChessTileParent { get; set; }
    public ITileContainer Container { get; set; }

    public override void SetObject(BasePawn objectToSlot, bool teleport = true)
    {
        base.SetObject(objectToSlot, teleport);
        this.slotedObject.transform.parent = this.transform;
        objectToSlot.slotParent = this;

        if (teleport == true)
        {
            this.slotedObject.transform.localPosition = Vector3.zero;
            this.slotedObject.transform.rotation = this.transform.rotation;
        }
    }
}
