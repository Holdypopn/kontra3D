using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Handles slot selected button click
/// </summary>
public class SlotSelectHandler : MonoBehaviour
{
    /// <summary>
    /// Takes the click event from the border of the slot and updates the inventory
    /// </summary>
    /// <param name="borderOfSlot"></param>
    public void OnSlotClick()
    {
        Inventory.inventoryInstance.CurrentSelectedSlot = Int32.Parse(transform.parent.name.Split('(')[1].Split(')')[0]); //Naming convention of slot is mandatory: f.e. Slot (1)

        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
    }
}
