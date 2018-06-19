﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Inventory Inventory;

    // Use this for initialization
    void Start()
    {
        Inventory.ItemAdded += InventoryScript_ItemAdded;
    }

    private void InventoryScript_ItemAdded(object sender, InventoryEventsArgs e)
    {
        Transform inventoryPanel = transform.Find("Inventory").Find("InventoryPanel"); //TODO naming abhängig

        int index = -1;
        foreach (Transform slot in inventoryPanel)
        {
            index++;

            // Border... Image
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Transform textTransform = slot.GetChild(0).GetChild(1);
            Image image = imageTransform.GetComponent<Image>();
            Text txtCount = textTransform.GetComponent<Text>();

            if (index == e.Item.Slot.Id)
            {
                image.enabled = true;

                image.sprite = Resources.Load<Sprite>(e.Item.Name + "Icon"); ;

                int itemCount = e.Item.Slot.Count;
                if (itemCount > 1)
                    txtCount.text = itemCount.ToString();
                else
                    txtCount.text = "";

                break;
            }
        }
    }
}
