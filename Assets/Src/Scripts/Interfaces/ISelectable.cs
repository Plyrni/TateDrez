using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable
{
    bool IsSelected { get; }

    public void OnSelect();
    public void OnUnSelect();
}
