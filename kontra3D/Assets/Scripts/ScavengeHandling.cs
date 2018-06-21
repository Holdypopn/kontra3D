using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengeHandling : MonoBehaviour {

    	Player player;
	Inventory inventory;
	List<InventoryItem_Base> itemList = inventoryInstance.availableItems;

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
}
