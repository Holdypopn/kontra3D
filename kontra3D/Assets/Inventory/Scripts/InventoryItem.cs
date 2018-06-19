using System;
using System.Reflection;
using UnityEditor.Playables;
using UnityEngine;

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

    [NonSerialized]
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

    //TODO if statements ??
    public virtual string GetHoverMenue()
    {
        var infos = "Properties of Item:\n";

        if(this is InventoryItem_Drink)
        {
            infos += "Drink\nUse this item if you are thirsty\nDrinkpoints: " + ((InventoryItem_Drink)this).DrinkPoints;
        }
        else if (this is InventoryItem_Food)
        {
            infos += "Food\nUse this item if you are hungry\nFoodpoints: " + ((InventoryItem_Food)this).FoodPoints;
        }
        else if (this is InventoryItem_Health)
        {
            infos += "Health\nUse this item to regenerate health\nHealthpoints: " + ((InventoryItem_Health)this).HealthPoints;
        }
        else if (this is InventoryItem_Weapon)
        {
            infos += "Weapon\nUse this item to damage enemies\nWeaponpoints: " + ((InventoryItem_Weapon)this).WeaponPoints;
        }

        return infos;
    }
    private void GetDescription(string type, string description, string points)
    { }
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