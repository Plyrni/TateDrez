using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ASlot<T> : MonoBehaviour
{
    public T slotedObject;
    public bool IsEmpty => this.slotedObject == null;

    public virtual void SetObject(T gameObject, bool teleport = true)
    {
        if (this.slotedObject != null)
        {
            Debug.LogWarning("Uhhh, you are overriding a slotedObject." + slotedObject);
            this.UnsetObject();
        }
        this.slotedObject = gameObject;
    }
    public virtual void UnsetObject()
    {
        this.slotedObject = default(T);
    }
}
