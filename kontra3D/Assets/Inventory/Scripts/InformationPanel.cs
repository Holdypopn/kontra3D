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
        Inventory.inventoryInstance.ItemRemoved += InventoryScript_ItemRemoved;
    }

    private void InventoryScript_ItemRemoved(object sender, InventoryEventsArgs e)
    {
        if (Inventory.inventoryInstance.CurrentSelectedSlot == e.Item.Slot.Id && e.Item.Slot.Count == 0)
        {
            var image = transform.GetChild(1).GetComponent<Image>();
            var description = transform.GetChild(2).GetComponent<Text>();

            description.text = "";
            image.sprite = null;
        }
    }

    private void InventoryScript_ItemSelected(object sender, InventoryEventsArgs e)
    {
        var image = transform.GetChild(1).GetComponent<Image>();
        var description = transform.GetChild(2).GetComponent<Text>();

        if (e.Item != null)
        {
            description.text = e.Item.Description;
            image.sprite = Resources.Load<Sprite>(e.Item.Name + "Icon");
        }
        else
        {
            description.text = "";
            image.sprite = null;
        }
    }

    public void OnUseClick()
    {
        Inventory.inventoryInstance.UseSelectedItem();
    }

    public void OnRemoveClick()
    {
        Inventory.inventoryInstance.RemoveSelectedItem();
    }
}
