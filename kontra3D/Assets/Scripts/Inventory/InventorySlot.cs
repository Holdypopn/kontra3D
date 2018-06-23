using System;
using System.Collections.Generic;


/// <summary>
/// No monohavior class - Only Model class (no binding to game object)
///     - Defines a slot with ID and size which allows to stack items in a slot
///     - Info about the stack count
/// </summary>
public class InventorySlot : ICloneable
{
    //Contains the Stack of the slot
    public Stack<InventoryItem_Base> ItemStack = new Stack<InventoryItem_Base>();

    //Id of the slot
    private int id = 0;

    /// <summary>
    /// Creates a new slot with ID
    /// </summary>
    /// <param name="id"></param>
    public InventorySlot(int id)
    {
        this.id = id;
    }

    /// <summary>
    /// Id of the slot
    /// </summary>
    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    /// <summary>
    /// Adds a item to the stack
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(InventoryItem_Base item)
    {
        item.Slot = this;
        ItemStack.Push(item);
    }

    /// <summary>
    /// Get the FirstItem of the Stack
    /// </summary>
    public InventoryItem_Base FirstItem
    {
        get
        {
            if (IsEmpty)
                return null;

            return ItemStack.Peek();
        }
    }

    /// <summary>
    /// Check if a Item could be added or if stack already reached max size
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool IsStackable(InventoryItem_Base item)
    {
        if (IsEmpty || item.StackCount == ItemStack.Count)
            return false;

        InventoryItem_Base first = ItemStack.Peek();

        if (first.Name == item.Name)
            return true;

        return false;
    }

    /// <summary>
    /// Checks if a slot is empty
    /// </summary>
    public bool IsEmpty
    {
        get { return Count == 0; }
    }

    /// <summary>
    /// Get the size of the stack
    /// </summary>
    public int Count
    {
        get { return ItemStack.Count; }
    }

    /// <summary>
    /// Remove a item from the stack
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool Remove(InventoryItem_Base item)
    {
        if (IsEmpty)
            return false;

        InventoryItem_Base first = ItemStack.Peek();
        if (first.Name == item.Name)
        {
            ItemStack.Pop();
            return true;
        }
        return false;
    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
