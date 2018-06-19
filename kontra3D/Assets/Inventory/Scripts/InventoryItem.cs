using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//public interface IInventoryItem
//{
//    string Description { get; }

//    string Name { get; }

//    Sprite Image { get; }

//    InventorySlot Slot { get; set; }

//    int StackCount { get; }
//}

public class InventoryEventsArgs : EventArgs
{
    public InventoryEventsArgs(InventoryItem_Base item)
    {
        Item = item;
    }

    public InventoryItem_Base Item;
}

[Serializable]
public class InventoryItems
{
    public InventoryItem_Food[] Food;
    public InventoryItem_Drink[] Drink;
    public InventoryItem_Weapon[] Weapon;
    public InventoryItem_Health[] Health;
}

[Serializable]
public class InventoryItem_Base
{
    public string Name;
    public string Description;
    public int StackCount;

    public Sprite Image //Maybe not working during parse
    {
        get
        {
            Debug.Log("Uff: " + Name + "Icon");
            return Resources.Load<Sprite>(Name + "Icon");
        }
    }

    [NonSerialized]
    private InventorySlot slot; //Maybe parsing not working
    public InventorySlot Slot //Maybe parsing not working
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
}

[Serializable]
public class InventoryItem_Drink : InventoryItem_Base
{
    public int DrinkPoints;
}

[Serializable]
public class InventoryItem_Food : InventoryItem_Base
{
    public int FoodPoints;
}

[Serializable]
public class InventoryItem_Health : InventoryItem_Base
{
    public int HealthPoints;
}

[Serializable]
public class InventoryItem_Weapon : InventoryItem_Base
{
    public string WeaponPoints;
}

//public class InventoryItem_Base : IInventoryItem
//{
//    public InventoryItem_Base(string name, string description, int stackCount)
//    {
//        this._name = name;
//        this.description = description;
//        this.image = Resources.Load<Sprite>(name + "Icon");
//        this.stackCount = stackCount;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
//    }

//    private string _name;
//    public string Name
//    {
//        get
//        {
//            return _name;
//        }
//    }

//    private Sprite image;

//    public Sprite Image
//    {
//        get
//        {
//            return image;
//        }
//    }

//    private string description;
//    public string Description
//    {
//        get
//        {
//            return description;
//        }
//    }

//    private InventorySlot slot;
//    public InventorySlot Slot
//    {
//        get
//        {
//            return slot;
//        }
//        set
//        {
//            slot = value;
//        }
//    }

//    private int stackCount;
//    public int StackCount
//    {
//        get
//        {
//            return stackCount;
//        }
//    }

//    public void OnPickup()
//    {
//        Debug.Log("Item pick up");
//    }
//}
