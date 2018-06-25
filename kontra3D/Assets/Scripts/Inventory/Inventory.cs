using System;
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
    public static Inventory Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        Instance = this;
    }
    #endregion

    /// <summary>
    /// Inventory transform
    /// </summary>
    public Transform Transform;

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
    public IList<InventorySlot> Slots = new List<InventorySlot>();

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
    /// Called if item slots are changed
    /// </summary>
    public event EventHandler<InventoryChangeEventsArgs> ItemSlotChanged;

    /// <summary>
    /// Event is called when a Item is selected in the Inventory
    /// </summary>
    private void OnItemSelected()
    {
        InventoryItem_Base item = Slots.Where(s => s.Id == currentSelectedSlot).First().FirstItem;

        if (ItemSelected != null)
            ItemSelected(this, new InventoryEventsArgs(item));
    }

    public void ChangeItems(int id1, int id2)
    {
        //Change Item stacks
        var slot1ItemStack = Slots[id1].ItemStack;
        Slots[id1].ItemStack = Slots[id2].ItemStack;
        Slots[id2].ItemStack = slot1ItemStack;

        //Change item slots
        foreach (var item in Slots[id1].ItemStack)
        {
            item.Slot = Slots[id1];
        }

        foreach (var item in Slots[id2].ItemStack)
        {
            item.Slot = Slots[id2];
        }

        if (ItemSlotChanged != null)
            ItemSlotChanged(this, new InventoryChangeEventsArgs(Slots));
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

    public InventoryItem_Base GetSelectedItem()
    {
        return Slots[currentSelectedSlot].FirstItem;
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
        AvailableItems.AddRange(temp.Equipment);
        AvailableItems.AddRange(temp.Health);
    }

    /// <summary>
    /// Creates the Inventory with the defined number of slots
    /// </summary>
    public Inventory()
    {
        for (int i = 0; i < SLOTS; i++)
        {
            Slots.Add(new InventorySlot(i));
        }
    }

    /// <summary>
    /// Finds the next stackable slot for an item
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private InventorySlot FindStackableSlot(InventoryItem_Base item)
    {
        foreach (InventorySlot slot in Slots)
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
        foreach (InventorySlot slot in Slots)
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
    /// Add item on positon
    /// </summary>
    /// <param name="name"></param>
    public void AddItem(string name, int id)
    {
        InventoryItem_Base item = null;

        item = GetItem(name);

        if (item == null)
        {
            Debug.Log("The item " + name + "does not exist in available items.");
        }

        InventorySlot freeSlot = Slots[id];

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
    public InventoryItem_Base RemoveItem(int id)
    {
        var slot = Slots.Where(s => s.Id == id).First();
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
