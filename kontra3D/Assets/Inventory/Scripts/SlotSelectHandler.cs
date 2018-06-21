using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlotSelectHandler : MonoBehaviour
{
   public void OnClick(Transform slot)
   {
      Inventory.inventoryInstance.CurrentSelectedSlot = Int32.Parse(slot.name.Split("(")[1].Split(")")[0]) //TODO needs naming convetnion of slots
   }
}
