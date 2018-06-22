﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Binding to the Inventory
///     - Contains a list of all available Items parsed from json
///     - Add and Remove functions for the inventory
///     
/// </summary>
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

    //Contains a list of all available items
    public List<InventoryItem_Base> AvailableItems;
    
    private int currentSelectedSlot = -1;
    /// <summary>
    /// Current selected slot
    /// </summary>
    public int CurrentSelectedSlot
    {
        get
        {
            return currentSelectedSlot;
        }
        set
        {
            currentSelectedSlot = value;
            OnItemSelected();
        }
    }

    //Slot count of the inventory
    private const int SLOTS = 16;

    //Contains all slots of the inventory
    private IList<InventorySlot> mSlots = new List<InventorySlot>();

    /// <summary>
    /// Called if Item is added to the inventory
    /// </summary>
    public event EventHandler<InventoryEventsArgs> ItemAdded;

    /// <summary>
    /// Called if item is selected in the inventory
    /// </summary>
    public event EventHandler<InventoryEventsArgs> ItemSelected;

    /// <summary>
    /// Called if item is removed from the inventory
    /// </summary>
    public event EventHandler<InventoryEventsArgs> ItemRemoved;

    /// <summary>
    /// Called if item is used and removed from the inventory
    /// </summary>
    public event EventHandler<InventoryEventsArgs> ItemUsed;
    
    /// <summary>
    /// Event is called when a Item is selected in the Inventory
    /// </summary>
    private void OnItemSelected()
    {
        InventoryItem_Base item = mSlots.Where(s => s.Id == currentSelectedSlot).First().FirstItem;

        if (ItemSelected != null)
            ItemSelected(this, new InventoryEventsArgs(item));
    }

    /// <summary>
    /// Remove the current selected item
    /// </summary>
    public void RemoveSelectedItem()
    {
        if(currentSelectedSlot != -1)
            RemoveItem(currentSelectedSlot);
    }

    /// <summary>
    /// Use the current selected item
    /// </summary>
    public void UseSelectedItem()
    {
        InventoryItem_Base item = null;
        if (currentSelectedSlot != -1)
           item = RemoveItem(currentSelectedSlot);

        if (item!= null && ItemUsed != null)
            ItemUsed(this, new InventoryEventsArgs(item));
    }

    /// <summary>
    /// Read all available Items from the JSON file
    /// </summary>
    void Start()
    {
        var temp = JsonInventoryReader.GetItems();

        //Sums up all Items of the List
        AvailableItems.AddRange(temp.Drink);
        AvailableItems.AddRange(temp.Food);
        AvailableItems.AddRange(temp.Weapon);
        AvailableItems.AddRange(temp.Miscellaneous);
        AvailableItems.AddRange(temp.Health);
    }

    /// <summary>
    /// Creates the Inventory with the defined number of slots
    /// </summary>
    public Inventory()
    {
        for (int i = 0; i < SLOTS; i++)
        {
            mSlots.Add(new InventorySlot(i));
        }
    }

    /// <summary>
    /// Finds the next stackable slot for an item
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private InventorySlot FindStackableSlot(InventoryItem_Base item)
    {
        foreach (InventorySlot slot in mSlots)
        {
            if (slot.IsStackable(item))
                return slot;
        }
        return null;
    }

    /// <summary>
    /// Finds the next empty slot for an item
    /// </summary>
    /// <returns></returns>
    private InventorySlot FindNextEmptySlot()
    {
        foreach (InventorySlot slot in mSlots)
        {
            if (slot.IsEmpty)
                return slot;
        }
        return null;
    }

    /// <summary>
    /// Add a item to the inventory
    /// </summary>
    /// <param name="name"></param>
    public void AddItem(string name)
    {
        InventoryItem_Base item = null;

        item = GetItem(name);

        if (item == null)
        {
            Debug.Log("The item " + name + "does not exist in available items.");
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
    /// Remove a item from the inventory
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private InventoryItem_Base RemoveItem(int id)
    {
        var slot = mSlots.Where(s => s.Id == id).First();
        var item = slot.FirstItem;
        var ret = slot.Remove(item);

        if (ret && ItemRemoved != null)
        {
            ItemRemoved(this, new InventoryEventsArgs(item));
        }
        return item;
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
            foundItem = (InventoryItem_Base)AvailableItems.First(it => it.Name == type).Clone();
        }
        catch(Exception ex)
        {
            Debug.Log("Item not found: " + type);
            throw ex;
        }

        return foundItem;
    }
}