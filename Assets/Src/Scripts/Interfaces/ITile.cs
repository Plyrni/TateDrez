using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITile
{
    public GameObject gameObject { get; }
    public Vector2Int cellCoordinates { get; set; }
}
