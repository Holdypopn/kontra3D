using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationPanel : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        Inventory.inventoryInstance.ItemSelected += InventoryScript_ItemSelected;
    }
    
    private void InventoryScript_ItemSelected(object sender, InventoryEventsArgs e)
    {
        var image = transform.GetChild(1).GetComponent<Image>();
        var Description = transform.GetChild(2).GetComponent<Text>();

        if (e.Item != null)
        {
            Description.text = e.Item.Description;
            image.sprite = Resources.Load<Sprite>(e.Item.Name + "Icon");
        }
        else
        {
            Description.text = "";
            image.sprite = null;
        }
    }
}
