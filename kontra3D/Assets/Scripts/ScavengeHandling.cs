using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengeHandling : MonoBehaviour {

    	Player player;
	Inventory inventory;
	List<InventoryItem_Base> itemList = inventoryInstance.availableItems;
	
	enum FocusType
	{
		Food,
		Drink,
		Misc,
		Health
	};

	// Use this for initialization
	void Start ()
	{
        	player = Player.playerInstance;
		inventory = Inventory.inventoryInstance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void getRandomItem()
	{
		int totalRarity = itemList.Sum(x => x.Rarity);
		Random r = new Random();
		var randomNumber = r.NextDouble() * totalRarity;

		double totalSoFar = 0;
		foreach (var item in itemList)
		{
			totalSoFar += item.Rarity;
			if (totalSoFar > randomNumber)
			{
			    return item;
			}
		}
	}
	
	void getRandomItem(FocusType focus)
	{
		List<ICloneable> newList = new List<ICloneable>(itemList.Count);

		itemList.ForEach((item) =>
		    {
			newList.Add((ICloneable)item.Clone());
		    });
		
		newList.Where(i => i.GetType().Name.contains(focus.ToString())).ToList().ForEach(x => x.Rarity + 5)
		
		int totalRarity = newList.Sum(x => x.Rarity);
		Random r = new Random();
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
	}
}
