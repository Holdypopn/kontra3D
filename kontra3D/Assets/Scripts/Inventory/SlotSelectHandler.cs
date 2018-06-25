using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Handles slot selected button click
/// </summary>
public class SlotSelectHandler : MonoBehaviour
{
    public static Transform CurrentSelectedInventoryTransform;
    /// <summary>
    /// Takes the click event from the border of the slot and updates the inventory
    /// </summary>
    /// <param name="borderOfSlot"></param>
    public void OnSlotClick()
    {
        if (!transform.parent.parent.name.Contains("Equipment"))
        {
            Inventory.Instance.CurrentSelectedSlot = Int32.Parse(transform.parent.name.Split('(')[1].Split(')')[0]); //Naming convention of slot is mandatory: f.e. Slot (1)
            CurrentSelectedInventoryTransform = transform.parent;
        }
        else
        {
            Equipment.Instance.CurrentSelectedSlot = Int32.Parse(transform.parent.name.Split('(')[1].Split(')')[0]); //Naming convention of slot is mandatory: f.e. Slot (1)
        }

        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
    }
}
