using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strawberry : MonoBehaviour, IInventoryItem
{
    public string Name
    {
        get
        {
            return "Strawberry";
        }
    }

    public Sprite _Image = null;

    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }

    public string Description
    {
        get
        {
            return "This item will regenerate 20% hunger and 10% thirst.";
        }
    }

    private InventorySlot slot;
    public InventorySlot Slot
    {
        get
        {
            return slot;
        }

        set
        {
            slot = value;
        }
    }

    public int StackCount
    {
        get
        {
            return 2;
        }
    }

    public void OnPickup()
    {
        //????
    }
}
