using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITileInteractionController
{
    public void OnTouch(ITileInterractable tile);
}
