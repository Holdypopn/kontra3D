using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScavengeHandling : MonoBehaviour
{
    enum FocusType
    {
        Food,
        Drink,
        Misc,
        Health,
        Weapon,
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
            
            if (item.name == "NoneToggle")
            {
                noneToggle = item;
            }
        }
    }

    public void Scavenge()
    {
        if (!Player.playerInstance.Scavange(noneToggle.isOn ? 1 : 2))
        {
            Debug.Log("No AP for Scavange available");
            return;
        }

        FocusType f = FocusType.None;

        if (drinkToggle.isOn)
            f = FocusType.Drink;

        if (foodToggle.isOn)
            f = FocusType.Food;

        if (healthToggle.isOn)
            f = FocusType.Health;

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
        List<InventoryItem_Base> newList = new List<InventoryItem_Base>(Inventory.inventoryInstance.AvailableItems);
        
        if (focus != FocusType.None)
            newList.Where(i => i.GetType().Name.Contains(focus.ToString())).ToList().ForEach(x => x.Rarity += 5);

        int totalRarity = newList.Sum(x => x.Rarity);
        System.Random r = new System.Random();
        var randomNumber = r.NextDouble() * totalRarity;

        double totalSoFar = 0;
        foreach (var item in newList)
        {
            totalSoFar += item.Rarity;
            if (totalSoFar > randomNumber)
            {
                Debug.Log("found item: " + item.Name);
                return item;
            }
        }

        return null;
    }
}
