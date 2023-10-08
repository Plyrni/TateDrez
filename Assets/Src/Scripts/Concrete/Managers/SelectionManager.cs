using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SelectionManager
{
    public Stack<ISelectable> currentSelection = new Stack<ISelectable>();

    public void Select(ISelectable selectable)
    {
        if (this.currentSelection.Contains(selectable))
        {
            Debug.Log(selectable + "already selected");
            return;
        }
        this.currentSelection.Push(selectable);
        selectable.OnSelect();
    }
    public void Clear()
    {
        foreach (var item in currentSelection)
        {
            item?.OnUnSelect();
        }
        this.currentSelection.Clear();
    }

    public ISelectable GetLastSelected()
    {
        return this.currentSelection.Peek();
    }
}
