using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlotSelectHandler : MonoBehaviour
{
   public void OnClick()
   {
        Inventory.inventoryInstance.CurrentSelectedSlot = Int32.Parse(transform.parent.name.Split('(')[1].Split(')')[0]); //TODO needs naming convetnion of slots
        Debug.Log(Inventory.inventoryInstance.CurrentSelectedSlot);
   }
}
