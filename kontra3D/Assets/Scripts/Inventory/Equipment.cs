﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Equipment : MonoBehaviour {

    #region Singleton
    public static Equipment equipmentInstance;

    void Awake()
    {
        if (equipmentInstance != null)
        {
            Debug.LogWarning("More than one instance of Equipment found!");
            return;
        }

        equipmentInstance = this;
    }
    #endregion

    public Transform Transform;

    //Slot count of the equipment
    private const int SLOTS = 9;

    //Contains all slots of the inventory
    private IList<InventorySlot> Slots = new List<InventorySlot>();

    /// <summary>
    /// Called if Item is added to the inventory
    /// </summary>
    public event EventHandler<InventoryEventsArgs> ItemAdded;
    
    /// <summary>
    /// Called if item is removed from the inventory
    /// </summary>
    public event EventHandler<InventoryEventsArgs> ItemRemoved;

    /// <summary>
    /// Called if item slots are changed
    /// </summary>
    public event EventHandler<InventoryChangeEventsArgs> ItemSlotChanged;
        
    void Start()
    {
        Transform = transform;
    }

    /// <summary>
    /// Creates the Equipmetnt with the defined number of slots
    /// </summary>
    public Equipment()
    {
        for (int i = 0; i < SLOTS; i++)
        {
            Slots.Add(new InventorySlot(i));
        }
    }

    private bool CheckIfSlotAcceptsItem(InventoryItem_Equipment item, int slot)
    {
        Debug.Log(item.SlotName.Contains(((EquipmentSlots)slot).ToString()));
        return item.SlotName.Contains(((EquipmentSlots)slot).ToString());
    }

    /// <summary>
    /// Replaces a item
    /// </summary>
    /// <param name="name"></param>
    public InventoryItem_Base ReplaceItem(InventoryItem_Base item, int slot)
    {
        if(!(item is InventoryItem_Equipment))
        {
            return null;
        }

        InventoryItem_Equipment itemAsEquip = item as InventoryItem_Equipment;

        if(!CheckIfSlotAcceptsItem(itemAsEquip, slot))
        {
            return null;
        }
        
        var oldSlot = Slots[slot].FirstItem == null ? new InventoryItem_Base() { Name = "empty"} : Slots[slot].FirstItem.Clone() as InventoryItem_Base;

        Slots[slot].Remove(item);
        Slots[slot].AddItem(item);

        if (ItemAdded != null)
        {
            ItemAdded(this, new InventoryEventsArgs(item));
        }

        return oldSlot;
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

    public bool ChangeItems(int id1, int id2)
    {
        //Check if slots accets the other
        if (!(CheckIfSlotAcceptsItem(Slots[id1].FirstItem as InventoryItem_Equipment, id2)))
            return false;

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

        return true;
    }
}

enum EquipmentSlots
{
    Head = 0,
    Hand = 1,
    Han = 2,
    Chest = 3,
    Trouser = 4,
    Foot = 5,
    Special = 6,
    Specia = 7,
    Speci = 8 //TODO
}
