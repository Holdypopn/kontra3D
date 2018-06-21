using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScavengeHandling : MonoBehaviour
{

    Player player;
    Inventory inventory;
    tmpScavengeHelper scavengeHelper;
    //List<InventoryItem_Base> itemList = Inventory.inventoryInstance.AvailableItems;

    enum FocusType
    {
        Food,
        Drink,
        Misc,
        Health,
        Weapon,
        None
    };

    // Use this for initialization
    void Start()
    {
        player = Player.playerInstance;
        inventory = Inventory.inventoryInstance;
        scavengeHelper = tmpScavengeHelper.scavengeHelperInstance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Scavenge()
    {
        scavengeHelper.inputText.text = player.Health.ToString() + "\n" + player.Hunger.ToString() + "\n" + player.Thirst.ToString();

        FocusType f = FocusType.None;

        if (scavengeHelper.drinkToggle.isOn)
            f = FocusType.Drink;

        if (scavengeHelper.foodToggle.isOn)
            f = FocusType.Food;

        if (scavengeHelper.healthToggle.isOn)
            f = FocusType.Health;

        InventoryItem_Base foundItem = getRandomItem(f);
        
        scavengeHelper.outputText.text = foundItem.Name;
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
