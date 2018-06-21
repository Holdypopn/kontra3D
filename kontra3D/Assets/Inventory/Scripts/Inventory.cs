using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory inventoryInstance;

    void Awake()
    {
        if (inventoryInstance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        inventoryInstance = this;
    }
    #endregion

    //Contains the parsed information
    private List<InventoryItem_Base> availableItems;
        
    //Contains the current selected slot
    public int CurrentSelectedSlot = -1;

    private const int SLOTS = 16;

    private IList<InventorySlot> mSlots = new List<InventorySlot>();

    public event EventHandler<InventoryEventsArgs> ItemAdded;

    void Start()
    {
        var temp = JsonInventoryReader.GetItems();
        
        //TODO needs adaption on new Inventory type
        availableItems.AddRange(temp.Drink);
        availableItems.AddRange(temp.Food);
        availableItems.AddRange(temp.Weapon);
        availableItems.AddRange(temp.Miscellaneous);
        availableItems.AddRange(temp.Health);
    }

    public Inventory()
    {
        for (int i = 0; i < SLOTS; i++)
        {
            mSlots.Add(new InventorySlot(i));
        }
    }

    private InventorySlot FindStackableSlot(InventoryItem_Base item)
    {
        foreach (InventorySlot slot in mSlots)
        {
            if (slot.IsStackable(item))
                return slot;
        }
        return null;
    }

    private InventorySlot FindNextEmptySlot()
    {
        foreach (InventorySlot slot in mSlots)
        {
            if (slot.IsEmpty)
                return slot;
        }
        return null;
    }


    public void AddItem(string type)
    {
        InventoryItem_Base item = null;

        item = GetItem(type);

        if (item == null)
        {
            Debug.Log("The item " + type + "does not exist in available items.");
        }

        InventorySlot freeSlot = FindStackableSlot(item);

        if (freeSlot == null)
        {
            freeSlot = FindNextEmptySlot();
        }

        if (freeSlot != null)
        {
            freeSlot.AddItem(item);

            if (ItemAdded != null)
            {
                ItemAdded(this, new InventoryEventsArgs(item));
            }
        }
    }

    /// <summary>
    /// Gets the InventoryItem of an type
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private InventoryItem_Base GetItem(string type)
    {
        InventoryItem_Base foundItem = null;
        try
        {
            foundItem = a.First(it => it.Name == type);
        }
        catch(Exception ex)
        {
            Debug.Log("Item not found: " + type);
            throw ex;
        }

        return foundItem;
    }
}
