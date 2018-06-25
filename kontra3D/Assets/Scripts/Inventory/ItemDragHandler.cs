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
        foreach(RectTransform slot in Inventory.Instance.Transform.Find("InventoryPanel")) //InventoryPanel
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(slot, Input.mousePosition)) 
            {
                destinationSlot = slot;
                destination = "Inventory";
            }
        }

        foreach (RectTransform slot in Equipment.Instance.Transform)
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
                ChangeSlotsInEquipment(transform.parent.parent, destinationSlot);
            }
            else //Destination is Inventory
            {
                MoveFromInventoryToEquipment(destinationSlot, transform.parent.parent);
            }
        }
        else //Source is Inventory
        {
            if (destination == "Inventory") //Destination is Inventory
            {
                ChangeSlotsInInventory(transform.parent.parent, destinationSlot);
            }
            else //Destination is Equipment
            {
                MoveFromInventoryToEquipment(transform.parent.parent, destinationSlot);
            }
        }
    }

    private void ChangeSlotsInInventory(Transform t1, Transform t2)
    {
        var slotId1 = Int32.Parse(t1.name.Split('(')[1].Split(')')[0]); //Naming convention of slot is mandatory: f.e. Slot (1)
        var slotId2 = Int32.Parse(t2.name.Split('(')[1].Split(')')[0]); //Naming convention of slot is mandatory: f.e. Slot (1)

        Inventory.Instance.ChangeItems(slotId1, slotId2);

        Inventory.Instance.CurrentSelectedSlot = slotId2;
    }

    private void ChangeSlotsInEquipment(Transform t1, Transform t2)
    {
        var slotId1 = Int32.Parse(t1.name.Split('(')[1].Split(')')[0]); //Naming convention of slot is mandatory: f.e. Slot (1)
        var slotId2 = Int32.Parse(t2.name.Split('(')[1].Split(')')[0]); //Naming convention of slot is mandatory: f.e. Slot (1)

        if(!(Equipment.Instance.ChangeItems(slotId1, slotId2)))
            Debug.Log("Items could not be changed!");
    }

    private void MoveFromInventoryToEquipment(Transform inv, Transform equip)
    {
        var slotIdInv = Int32.Parse(inv.name.Split('(')[1].Split(')')[0]); //Naming convention of slot is mandatory: f.e. Slot (1)
        var slotIdEquip = Int32.Parse(equip.name.Split('(')[1].Split(')')[0]); //Naming convention of slot is mandatory: f.e. Slot (1)

        InventoryItem_Base itemFromEquip = null;

        if (Inventory.Instance.Slots[slotIdInv].FirstItem != null)
            itemFromEquip = Equipment.Instance.ReplaceItem(Inventory.Instance.Slots[slotIdInv].FirstItem.Clone() as InventoryItem_Base, slotIdEquip);
        else
            itemFromEquip = Equipment.Instance.RemoveItem(slotIdEquip);

        if (itemFromEquip != null && itemFromEquip.Name == "empty")//Slot was empty
        {
            Inventory.Instance.RemoveItem(slotIdInv);
            return;
        }
        else if (itemFromEquip != null)
        {
            Inventory.Instance.RemoveItem(slotIdInv);
            Inventory.Instance.AddItem(itemFromEquip.Name, slotIdInv);
        }
        else
            Debug.Log("The slot did not accept this item");
    }
}
