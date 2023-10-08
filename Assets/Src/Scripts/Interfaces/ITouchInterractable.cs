using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouchInterractable
{
    /// <summary>
    /// Called when a tileInterractable gets clicked or touched
    /// </summary>
    public void OnTouch();
    public void EnableInterraction(bool enable);
}