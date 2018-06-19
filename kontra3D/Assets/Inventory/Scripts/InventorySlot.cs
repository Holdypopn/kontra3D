using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot
{
    private Stack<InventoryItem_Base> mItemStack = new Stack<InventoryItem_Base>();

    private int mId = 0;

    public InventorySlot(int id)
    {
        mId = id;
    }

    public int Id
    {
        get { return mId; }
    }

    public void AddItem(InventoryItem_Base item)
    {
        item.Slot = this;
        mItemStack.Push(item);
    }

    public InventoryItem_Base FirstItem
    {
        get
        {
            if (IsEmpty)
                return null;

            return mItemStack.Peek();
        }
    }

    public bool IsStackable(InventoryItem_Base item)
    {
        if (IsEmpty || item.StackCount == mItemStack.Count)
            return false;

        InventoryItem_Base first = mItemStack.Peek();

        if (first.Name == item.Name)
            return true;

        return false;
    }

    public bool IsEmpty
    {
        get { return Count == 0; }
    }

    public int Count
    {
        get { return mItemStack.Count; }
    }

    public bool Remove(InventoryItem_Base item)
    {
        if (IsEmpty)
            return false;

        InventoryItem_Base first = mItemStack.Peek();
        if (first.Name == item.Name)
        {
            mItemStack.Pop();
            return true;
        }
        return false;
    }
}
