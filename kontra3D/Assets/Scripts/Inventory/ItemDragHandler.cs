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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void CheckForObjectUnderMouse()
    {
        foreach(RectTransform slot in transform.parent.parent.parent)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(slot, Input.mousePosition)) 
            {
                ChangeSlots(transform.parent.parent, slot);
            }
        }
    }

    private void ChangeSlots(Transform t1, Transform t2)
    {
        var slotId1 = Int32.Parse(t1.name.Split('(')[1].Split(')')[0]); //Naming convention of slot is mandatory: f.e. Slot (1)
        var slotId2 = Int32.Parse(t2.name.Split('(')[1].Split(')')[0]); //Naming convention of slot is mandatory: f.e. Slot (1)

        Inventory.inventoryInstance.ChangeItems(slotId1, slotId2);

        Inventory.inventoryInstance.CurrentSelectedSlot = slotId2;
    }
}
