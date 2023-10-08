using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileContainerOwnerType
{
    NONE = -1,
    GameBoard,
    PawnContainer,
}

public interface ITileContainerOwner
{
    TileContainerOwnerType Type { get; }
    ITileContainer TileContainer { get; }
}
