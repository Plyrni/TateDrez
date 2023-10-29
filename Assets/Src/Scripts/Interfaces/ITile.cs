using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public interface ITile
{
    public GameObject gameObject { get; }
    public Vector2Int cellCoordinates { get; set; }
    public void SetVisualScale(Vector3 newScale);
}
