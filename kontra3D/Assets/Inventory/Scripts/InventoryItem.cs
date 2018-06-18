using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem
{
    string Description { get; }

    string Name { get; }

    Sprite Image { get; }

    void OnPickup();

    InventorySlot Slot { get; set; }

    int StackCount { get; }
}

public class InventoryEventsArgs : EventArgs
{
    public InventoryEventsArgs(IInventoryItem item)
    {
        Item = item;
    }

    public IInventoryItem Item;
}
