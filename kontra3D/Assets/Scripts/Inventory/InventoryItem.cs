using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Eventclass for Inventory events
/// </summary>
public class InventoryEventsArgs : EventArgs
{
    public InventoryEventsArgs(InventoryItem_Base item)
    {
        Item = item;
    }

    public InventoryItem_Base Item;
}

/// <summary>
/// Eventclass for Inventory change events
/// </summary>
public class InventoryChangeEventsArgs : EventArgs
{
    public InventoryChangeEventsArgs(IList<InventorySlot> slots)
    {
        Slots = slots;
    }

    public IList<InventorySlot> Slots;
}


/// <summary>
/// Definition of InventoryItems as defined in the JSON script
/// </summary>
[Serializable]
public class InventoryItems
{
    public InventoryItem_Food[] Food;
    public InventoryItem_Drink[] Drink;
    public InventoryItem_Equipment[] Equipment;
    public InventoryItem_Health[] Health;
}

[Serializable]
public class InventoryItem_Base : ICloneable
{
    public string Name;
    public string Description;

    [HoverMenue(DisplayName = "Max. stack size")]
    public int StackCount;

    [HoverMenue(DisplayName = "Rarity of item")]
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
    
    public object Clone()
    {
        return this.MemberwiseClone();
    }

    /// <summary>
    /// Creates the Info for the Hover menu for the current item
    /// </summary>
    /// <returns></returns>
    public string GetHoverMenue()
    {
        string hoverInfo = "[" + this.GetType().Name.Split('_').Last() + "] " + this.Name + "\n"; // f. e. [Food] Steak

        //Get all properties for hover menue
        var properties = this.GetType().GetFields().Where(prop => prop.IsDefined(typeof(HoverMenue), false));
        
        properties.ToList().ForEach(p => hoverInfo += ((HoverMenue)p.GetCustomAttributes(typeof(HoverMenue), false).First()).DisplayName + " : " + p.GetValue(this) + "\n"); //f. e. DisplayName : Value

        return hoverInfo;
    }
}

[Serializable]
public class InventoryItem_Drink : InventoryItem_Base
{
    [HoverMenue(DisplayName = "Drink points")]
    public int DrinkPoints;
}

[Serializable]
public class InventoryItem_Food : InventoryItem_Base
{
    [HoverMenue(DisplayName = "Food points")]
    public int FoodPoints;
}

[Serializable]
public class InventoryItem_Health : InventoryItem_Base
{
    [HoverMenue(DisplayName = "Health points")]
    public int HealthPoints;
}

[Serializable]
public class InventoryItem_Equipment : InventoryItem_Base
{
    [HoverMenue(DisplayName ="Equipment slot")]
    public string SlotName;

    public EquipmentBenefits EquipmentBenefits;
}

[Serializable]
public class EquipmentBenefits
{
    public int ScavengeneItemMultiplier;

    /// <summary>
    /// Sums all EquipmentBenefit properties and returns one element with the summed info (Generic property read and sum properties of two EquipmentBenefits objects)
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static EquipmentBenefits operator +(EquipmentBenefits x, EquipmentBenefits y)
    {
        var temp = new EquipmentBenefits();

        //Get all properties of EquipmentBenefits
        var properties = x.GetType().GetFields();

        //Sum all properties of x and y, and store the sum in temp -> Generic sum of all properties
        properties.ToList().ForEach(p => p.SetValue(temp, ((int)p.GetValue(x)) + ((int)p.GetValue(y))));

        return temp;
    }
}

/// <summary>
/// Attribute class to define parameters in Hovermenu
/// </summary>
public class HoverMenue : System.Attribute
{
    public string DisplayName;
}
