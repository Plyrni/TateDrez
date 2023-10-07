using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage what happen when you touch a Tile
/// </summary>
public interface ITouchInteraction
{
    public void Execute(ITouchInterractable tile);
}
