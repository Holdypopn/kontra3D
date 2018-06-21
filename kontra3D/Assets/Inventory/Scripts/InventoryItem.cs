using System;
using System.Linq;
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
    public InventoryItem_Miscellaneous[] Miscellaneous;
    public InventoryItem_Health[] Health;
}

[Serializable]
public class InventoryItem_Base
{
    public string Name;
    public string Description;
    
    [HoverMenue]
    public int StackCount;

    [HoverMenue]
    public int Rarity;

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

    public string GetHoverMenue()
    {
        string hoverInfo = "[" + this.GetType().Name.Split('_').Last() + "] " + this.Name + "\n"; // f. e. [Food] Steak

        //Get all properties for hover menue
        var properties = this.GetType().GetFields().Where(prop => prop.IsDefined(typeof(HoverMenue), false));

        properties.ToList().ForEach(p => hoverInfo += p.Name + " : " + p.GetValue(this) + "\n");

        return hoverInfo;
    }
}

[Serializable]
public class InventoryItem_Drink : InventoryItem_Base
{
    [HoverMenue]
    public int DrinkPoints;
}

[Serializable]
public class InventoryItem_Food : InventoryItem_Base
{
    [HoverMenue]
    public int FoodPoints;
}

[Serializable]
public class InventoryItem_Health : InventoryItem_Base
{
    [HoverMenue]
    public int HealthPoints;
}

[Serializable]
public class InventoryItem_Miscellaneous : InventoryItem_Base
{
    [HoverMenue]
    public string MiscPoints;
}

[Serializable]
public class InventoryItem_Weapon : InventoryItem_Base
{
    [HoverMenue]
    public string WeaponPoints;
}

public class HoverMenue : System.Attribute { }
