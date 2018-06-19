using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //Contains the parsed information
    private InventoryItems availableItems;

    private const int SLOTS = 16;

    private IList<InventorySlot> mSlots = new List<InventorySlot>();

    public event EventHandler<InventoryEventsArgs> ItemAdded;

    void Start()
    {
        availableItems = JsonInventoryReader.GetItems();
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


    public void AddItem(ItemType type)
    {
        //var inventoryItemComponent = new GameObject().AddComponent<InventoryItem>();

        InventoryItem_Base item = null;
        //Get the item from all parsed items

        item = GetItem(type);

        //item = availableItems.Drink.First(it => it.Name == type.ToString());

        //for (int i=0; i< 4; i++)    //TODO alle listen durchitearieren und nicht mit for und fixen index
        //{
        //    switch(i)
        //    {
        //        case 0:
        //            item = availableItems.Food.First(it => it.Name == type.ToString());
        //            break;
        //        //case 1:
        //        //    item = availableItems.Health.First(it => it.Name == type.ToString());

        //        //    break;
        //        //case 2:
        //        //    item = availableItems.Weapon.First(it => it.Name == type.ToString());

        //        //    break;
        //        //case 3:
        //        //    item = availableItems.Drink.First(it => it.Name == type.ToString());
        //        //    break;
        //    }

        //    if(item != null)
        //    {
        //        break;
        //    }

        //}

        if (item == null)
        {
            Debug.Log("The item " + type.ToString() + "does not exist in available items.");
        }

        //Debug.Log("Found Item: " + parsedItem.Description);

        //if (parsedItem == null)
        //{
        //    Debug.Log("The item " + type.ToString() + "does not exist in available items.");
        //}

        //inventoryItemComponent.Name = parsedItem.Name;
        //inventoryItemComponent.Description = parsedItem.Description;
        //inventoryItemComponent.Image = Resources.Load<Sprite>(Application.dataPath + "//Inventory/Items//" + parsedItem.Name + ".png");
        //inventoryItemComponent.StackCount = parsedItem.StackCount;

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
    private InventoryItem_Base GetItem(ItemType type)
    {
        List<InventoryItem_Base> allProducts = new List<InventoryItem_Base>();

        allProducts.AddRange(availableItems.Drink);
        allProducts.AddRange(availableItems.Food);
        allProducts.AddRange(availableItems.Weapon);
        allProducts.AddRange(availableItems.Health);

        InventoryItem_Base foundItem = null;
        try
        {
            foundItem = allProducts.First(it => it.Name == type.ToString());
        }
        catch(Exception ex)
        {
            Debug.Log("Item not found: " + type.ToString());
            throw ex;
        }

        return foundItem;
    }
}

public enum ItemType
{
    Steak,
    Atibiotic,
    Knife,
    Applejuice,
    Strawberry
}
