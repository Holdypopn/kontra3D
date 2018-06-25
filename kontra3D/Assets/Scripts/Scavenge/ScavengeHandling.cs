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

	private Toggle foodToggle;
	private Toggle drinkToggle;
	private Toggle equipToggle;
	private Toggle healthToggle;
	private Toggle noneToggle;

	public int standardItemTypeProbability = 10;
	public int addedFocusProbability = 5;

	// Use this for initialization
	void Start()
	{
		setToggles();
	}
	
	/// <summary>
	/// Sets all found UI Toggles to all related programmed Toggles for further usage.
	/// </summary>
	private void setToggles()
	{
		var toggles = transform.GetComponentsInChildren<Toggle>();

		foreach (var item in toggles)
		{
			if (item.name == "FoodToggle")
			{
				foodToggle = item;
			}

			if (item.name == "DrinkToggle")
			{
				drinkToggle = item;
			}

			if (item.name == "EquipToggle")
			{
				equipToggle = item;
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
	
	/// <summary>
	/// Checks which Focus was set in the UI and adds the found random item to the inventory.
	/// </summary>
	public void Scavenge()
	{
        var benefits = Equipment.Instance.EquipmentBenefits;

		FocusType searchFocus = FocusType.None;

		if (drinkToggle.isOn)
	    		searchFocus = FocusType.Drink;
		else if (foodToggle.isOn)
		    	searchFocus = FocusType.Food;
		else if (healthToggle.isOn)
		    	searchFocus = FocusType.Health;
		else if (equipToggle.isOn)
		    searchFocus = FocusType.Equipment;

		if (!Player.playerInstance.Scavange(searchFocus == FocusType.None ? 1 : 2))
		{
			Debug.Log("No AP for Scavange available");
			return;
		}

		InventoryItem_Base foundItem = getRandomItem(searchFocus);

		Inventory.Instance.AddItem(foundItem.Name);
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

	// Fill list with dictionaries of FocusType and standardItemTypeProbability
        foreach (FocusType item in Enum.GetValues(typeof(FocusType)))
        {
            dictList.Add(new Dictionary<FocusType, int> { { item, standardItemTypeProbability } });
        }

        dictList.Remove(dictList.Single(x => x.Keys.First() == FocusType.None));

        int totalItemTypeProbability = 0;
        
	// Add additional focus probability and calculate totalItemTypeProbability
        foreach (var dict in dictList)
        {
            if (focus != FocusType.None)
            {
                if (dict.ContainsKey(focus))
                {
                    dict[focus] += addedFocusProbability;
                }
            }

            totalItemTypeProbability += dict.Sum(x => x.Value);
        }

        System.Random r = new System.Random();
        var randomNumber = r.NextDouble() * totalItemTypeProbability;
        double totalSoFar = 0;
		
	// Calculate foundItemType based on related probabilities
        foreach (var item in dictList)
        {
            totalSoFar += item.Sum(x => x.Value);
            if (totalSoFar > randomNumber)
            {
                foundItemType = item.Keys.First();
                break;
            }
        }
	Debug.Log("foundItemType: " + foundItemType.ToString());
        
        List<InventoryItem_Base> newList = new List<InventoryItem_Base>(Inventory.Instance.AvailableItems);
		
	// Filter list to only contain the specific item type based on the prior calculated foundItemType
        newList = newList.Where(i => i.GetType().Name.Contains(foundItemType.ToString())).ToList();

        Debug.Log("newList:");
        newList.ForEach(x => Debug.Log(x.Name));

        int totalItemRarity = newList.Sum(x => x.Rarity);
        System.Random rand = new System.Random();
        var randomItemNumber = rand.NextDouble() * totalItemRarity;
        totalSoFar = 0;
		
	// Calculate found item based on related Rarity
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
