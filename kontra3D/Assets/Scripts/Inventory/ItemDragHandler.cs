using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        transform.parent.Find("Text").GetComponent<Text>().enabled = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        transform.parent.Find("Text").GetComponent<Text>().enabled = true;
        CheckForObjectUnderMouse();
    }

    private void CheckForObjectUnderMouse()
    {
        RectTransform destinationSlot = null;
        string destination = "";

        //Change slots in Inventory
        foreach(RectTransform slot in Inventory.inventoryInstance.Transform.Find("InventoryPanel")) //InventoryPanel
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(slot, Input.mousePosition)) 
            {
                destinationSlot = slot;
                destination = "Inventory";
            }
        }

        foreach (RectTransform slot in Equipment.equipmentInstance.Transform)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(slot, Input.mousePosition))
            {
                destinationSlot = slot;
                destination = "Equipment";
            }
        }

        if (destinationSlot == null)
            return;

        if (transform.parent.parent.parent.name == "Equipment") //Source is Equipment
        {
            if (destination == "Equipment") //Destination is Equipment
            {
                Debug.Log("1");
                ChangeSlotsInEquipment(transform.parent.parent, destinationSlot);
            }
            else //Destination is Inventory
            {
                Debug.Log("2");

                MoveFromInventoryToEquipment(destinationSlot, transform.parent.parent);
            }
        }
        else //Source is Inventory
        {
            if (destination == "Inventory") //Destination is Inventory
            {
                Debug.Log("3");

                ChangeSlotsInInventory(transform.parent.parent, destinationSlot);
            }
            else //Destination is Equipment
            {
                Debug.Log("4");

                MoveFromInventoryToEquipment(transform.parent.parent, destinationSlot);
            }
        }
    }

    private void ChangeSlotsInInventory(Transform t1, Transform t2)
    {
        Debug.Log("ChangeSlotsInInventory");
        var slotId1 = Int32.Parse(t1.name.Split('(')[1].Split(')')[0]); //Naming convention of slot is mandatory: f.e. Slot (1)
        var slotId2 = Int32.Parse(t2.name.Split('(')[1].Split(')')[0]); //Naming convention of slot is mandatory: f.e. Slot (1)

        Inventory.inventoryInstance.ChangeItems(slotId1, slotId2);

        Inventory.inventoryInstance.CurrentSelectedSlot = slotId2;
    }

    private void ChangeSlotsInEquipment(Transform t1, Transform t2)
    {
        Debug.Log("ChangeSlotsInInventory");
        var slotId1 = Int32.Parse(t1.name.Split('(')[1].Split(')')[0]); //Naming convention of slot is mandatory: f.e. Slot (1)
        var slotId2 = Int32.Parse(t2.name.Split('(')[1].Split(')')[0]); //Naming convention of slot is mandatory: f.e. Slot (1)

        if(!(Equipment.equipmentInstance.ChangeItems(slotId1, slotId2)))
            Debug.Log("Items could not be changed!");
    }

    private void MoveFromInventoryToEquipment(Transform inv, Transform equip)
    {
        Debug.Log("MoveFromInventoryToEquipment");

        var slotIdInv = Int32.Parse(inv.name.Split('(')[1].Split(')')[0]); //Naming convention of slot is mandatory: f.e. Slot (1)
        var slotIdEquip = Int32.Parse(equip.name.Split('(')[1].Split(')')[0]); //Naming convention of slot is mandatory: f.e. Slot (1)

        InventoryItem_Base itemFromEquip = null;

        if (Inventory.inventoryInstance.Slots[slotIdInv].FirstItem != null)
            itemFromEquip = Equipment.equipmentInstance.ReplaceItem(Inventory.inventoryInstance.Slots[slotIdInv].FirstItem.Clone() as InventoryItem_Base, slotIdEquip);
        else
            itemFromEquip = Equipment.equipmentInstance.RemoveItem(slotIdEquip);

        if (itemFromEquip != null && itemFromEquip.Name == "empty")//Slot was empty
        {
            Inventory.inventoryInstance.RemoveItem(slotIdInv);
            return;
        }
        else if (itemFromEquip != null)
        {
            Debug.Log("Override " + itemFromEquip.Name);
            Inventory.inventoryInstance.RemoveItem(slotIdInv);
            Inventory.inventoryInstance.AddItem(itemFromEquip.Name, slotIdInv);
        }
        else
            Debug.Log("The slot did not accept this item");
    }
}
