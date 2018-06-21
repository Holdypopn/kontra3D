using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScavengeHandling : MonoBehaviour
{

    Player player;
    Inventory inventory;
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Returns a random Item
    /// </summary>
    /// <param name="focus">You can Focus on a Item to have a higher chance to get this</param>
    /// <returns></returns>
    InventoryItem_Base getRandomItem(FocusType focus = FocusType.None)
    {
        List<InventoryItem_Base> newList = new List<InventoryItem_Base>(Inventory.inventoryInstance.AvailableItems);

        newList = newList.Where(i => i.GetType().Name.Contains(focus.ToString())).ToList();
        newList.ForEach(x => x.Rarity += 5);

        int totalRarity = newList.Sum(x => x.Rarity);
        System.Random r = new System.Random();
        var randomNumber = r.NextDouble() * totalRarity;

        double totalSoFar = 0;
        foreach (var item in newList)
        {
            totalSoFar += item.Rarity;
            if (totalSoFar > randomNumber)
            {
                return item;
            }
        }

        return null;
    }
}
