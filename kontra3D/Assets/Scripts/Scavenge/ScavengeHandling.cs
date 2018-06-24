using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScavengeHandling : MonoBehaviour
{
    //TODO Kann man das vll generisch aus dem json erzeugen?
    enum FocusType
    {
        Food,
        Drink,
        Equipment,
        Health,
        None
    };
    
    private Toggle drinkToggle;
    private Toggle foodToggle;
    private Toggle healthToggle;
    private Toggle noneToggle;

    // Use this for initialization
    void Start()
    {
        var toggles = transform.GetComponentsInChildren<Toggle>();

        foreach (var item in toggles)
        {
            if (item.name == "DrinkToggle")
            {
                drinkToggle = item;
            }

            if (item.name == "FoodToggle")
            {
                foodToggle = item;
            }

            if (item.name == "HealthToggle")
            {
                healthToggle = item;
            }
        }
    }

    public void Scavenge()
    {
        FocusType f = FocusType.None;

        if (drinkToggle.isOn)
            f = FocusType.Drink;
        else if (foodToggle.isOn)
            f = FocusType.Food;
        else if (healthToggle.isOn)
            f = FocusType.Health;
        
        if (!Player.playerInstance.Scavange(f == FocusType.None ? 1 : 2))
        {
            Debug.Log("No AP for Scavange available");
            return;
        }

        InventoryItem_Base foundItem = getRandomItem(f);

        Inventory.inventoryInstance.AddItem(foundItem.Name);
    }

    /// <summary>
    /// Returns a random Item
    /// </summary>
    /// <param name="focus">You can Focus on a Item to have a higher chance to get this</param>
    /// <returns></returns>
    InventoryItem_Base getRandomItem(FocusType focus = FocusType.None)
    {
        FocusType foundItemType = FocusType.None;
        List<Dictionary<FocusType, int>> dictList = new List<Dictionary<FocusType, int>>();

        foreach (var item in Enum.GetValues(typeof(FocusType)))
        {
            dictList.Add(new Dictionary<FocusType, int> { { (FocusType)item, 10 } });
        }

        dictList.Remove(dictList.Single(x => x.Keys.First() == FocusType.None));

        int totalItemTypeRarity = 0;
        
        foreach (var dict in dictList)
        {
            if (focus != FocusType.None)
            {
                if (dict.ContainsKey(focus))
                {
                    dict[focus] += 5;
                }
            }

            totalItemTypeRarity += dict.Sum(x => x.Value);
        }

        System.Random r = new System.Random();
        var randomNumber = r.NextDouble() * totalItemTypeRarity;

        double totalSoFar = 0;
        foreach (var item in dictList)
        {
            totalSoFar += item.Sum(x => x.Value);
            if (totalSoFar > randomNumber)
            {
                foundItemType = item.Keys.First();
                break;
            }
        }
        
        List<InventoryItem_Base> newList = new List<InventoryItem_Base>(Inventory.inventoryInstance.AvailableItems);

        Debug.Log("foundItemType: " + foundItemType.ToString());
        newList = newList.Where(i => i.GetType().Name.Contains(foundItemType.ToString())).ToList();

        Debug.Log("newList:");
        newList.ForEach(x => Debug.Log(x.Name));

        int totalItemRarity = newList.Sum(x => x.Rarity);
        System.Random rand = new System.Random();
        var randomItemNumber = rand.NextDouble() * totalItemRarity;

        totalSoFar = 0;
        foreach (var item in newList)
        {
            totalSoFar += item.Rarity;
            if (totalSoFar > randomItemNumber)
            {
                Debug.Log("found item: " + item.Name);
                return item;
            }
        }

        return null;
    }
}
