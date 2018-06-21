using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    private int lastSelectedId = 0;
    // Use this for initialization
    void Start()
    {
        Inventory.inventoryInstance.ItemAdded += InventoryScript_ItemAdded;
        Inventory.inventoryInstance.ItemRemoved += InventoryScript_ItemRemoved;
        Inventory.inventoryInstance.ItemSelected += InventoryScript_ItemSelected;

        ////Bring all Hover menues to front
        //foreach (Transform slot in transform.Find("Inventory").Find("InventoryPanel"))//TODO naming abhängig
        //{
        //    Debug.Log(slot.name);
        //    slot.GetChild(0).SetAsLastSibling();
        //    //slot.GetChild(1).SetAsLastSibling();
    }

    private void InventoryScript_ItemSelected(object sender, InventoryEventsArgs e)
    {
        var colors = transform.GetChild(lastSelectedId).GetChild(1).GetComponent<Button>().colors;
        colors.normalColor = Color.white;
        transform.GetChild(lastSelectedId).GetChild(1).GetComponent<Button>().colors = colors;

        if (e.Item != null && e.Item.Slot != null)
        {
            colors = transform.GetChild(e.Item.Slot.Id).GetChild(1).GetComponent<Button>().colors;
            colors.normalColor = Color.blue;
            transform.GetChild(e.Item.Slot.Id).GetChild(1).GetComponent<Button>().colors = colors;
            lastSelectedId = e.Item.Slot.Id;
        }
    }

    private void InventoryScript_ItemRemoved(object sender, InventoryEventsArgs e)
    {
        Transform slot = transform.GetChild(e.Item.Slot.Id);

        // Border... Image TODO Getchild BAD PRACTICE!
        Transform imageTransform = slot.GetChild(1).GetChild(0);
        Transform textTransform = slot.GetChild(1).GetChild(1);
        Image image = imageTransform.GetComponent<Image>();
        Text txtCount = textTransform.GetComponent<Text>();
        Text HoverText = slot.GetChild(0).GetChild(0).GetComponent<Text>();

        
        int itemCount = e.Item.Slot.Count;

        if (itemCount == 0) //Is the last item
        {
            image.enabled = false;
            image.sprite = null;
            HoverText.text = "";
        }
        else if (itemCount == 1) //One is still there after deletion
        {
            txtCount.text = "";
        }
        else
        {
            txtCount.text = (itemCount -1).ToString();
        }
    }

    private void InventoryScript_ItemAdded(object sender, InventoryEventsArgs e)
    {
        int index = -1;
        foreach (Transform slot in transform)
        {
            index++;
                    
            // Border... Image TODO Getchild BAD PRACTICE!
            Transform imageTransform = slot.GetChild(1).GetChild(0);
            Transform textTransform = slot.GetChild(1).GetChild(1);
            Image image = imageTransform.GetComponent<Image>();
            Text txtCount = textTransform.GetComponent<Text>();
            Text HoverText = slot.GetChild(0).GetChild(0).GetComponent<Text>();

            if (index == e.Item.Slot.Id)
            {
                image.enabled = true;

                image.sprite = Resources.Load<Sprite>(e.Item.Name + "Icon");

                int itemCount = e.Item.Slot.Count;
                if (itemCount > 1)
                    txtCount.text = itemCount.ToString();
                else
                    txtCount.text = "";

                HoverText.text = e.Item.GetHoverMenue();
                
                break;
            }
        }
    }
}
