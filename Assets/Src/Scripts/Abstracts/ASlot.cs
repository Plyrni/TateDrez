using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ASlot : MonoBehaviour
{
    public GameObject slotedObject;
    public bool IsEmpty => this.slotedObject == null;
}
