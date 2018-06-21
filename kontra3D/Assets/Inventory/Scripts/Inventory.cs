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
    public List<InventoryItem_Base> AvailableItems;

    //Contains the current selected slot
    private int currentSelectedSlot = -1;
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

    private void OnItemSelected()
    {
        InventoryItem_Base item = mSlots.Where(s => s.Id == currentSelectedSlot).First().FirstItem;

        if (ItemSelected != null)
            ItemSelected(this, new InventoryEventsArgs(item));
    }

    public void RemoveSelectedItem()
    {
        if(currentSelectedSlot != -1)
            RemoveItem(currentSelectedSlot);
    }

    public void UseSelectedItem()
    {
        InventoryItem_Base item = null;
        if (currentSelectedSlot != -1)
           item = RemoveItem(currentSelectedSlot);

        if (item!= null && ItemUsed != null)
            ItemUsed(this, new InventoryEventsArgs(item));
    }

    private const int SLOTS = 16;

    private IList<InventorySlot> mSlots = new List<InventorySlot>();

    public event EventHandler<InventoryEventsArgs> ItemAdded;
    public event EventHandler<InventoryEventsArgs> ItemSelected;
    public event EventHandler<InventoryEventsArgs> ItemRemoved;
    public event EventHandler<InventoryEventsArgs> ItemUsed;

    void Start()
    {
        var temp = JsonInventoryReader.GetItems();
        
        //TODO needs adaption on new Inventory type
        AvailableItems.AddRange(temp.Drink);
        AvailableItems.AddRange(temp.Food);
        AvailableItems.AddRange(temp.Weapon);
        AvailableItems.AddRange(temp.Miscellaneous);
        AvailableItems.AddRange(temp.Health);
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
