using System.Collections;
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

        ////Bring all Hover menues to front
        //foreach (Transform slot in transform.Find("Inventory").Find("InventoryPanel"))//TODO naming abhängig
        //{
        //    Debug.Log(slot.name);
        //    slot.GetChild(0).SetAsLastSibling();
        //    //slot.GetChild(1).SetAsLastSibling();
        //}

    }

    private void InventoryScript_ItemAdded(object sender, InventoryEventsArgs e)
    {
        Transform inventoryPanel = transform.Find("Inventory").Find("InventoryPanel"); //TODO naming abhängig

        int index = -1;
        foreach (Transform slot in inventoryPanel)
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

                image.sprite = Resources.Load<Sprite>(e.Item.Name + "Icon"); ;

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
