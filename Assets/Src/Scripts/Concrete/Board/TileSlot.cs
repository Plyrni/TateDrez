using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSlot : ASlot
{
    public void SetObject(GameObject gameObject)
    {
        this.slotedObject = gameObject;
        this.slotedObject.transform.parent = this.transform;
        this.slotedObject.transform.localPosition = Vector3.zero;
        this.slotedObject.transform.rotation = this.transform.rotation;
    }
}
