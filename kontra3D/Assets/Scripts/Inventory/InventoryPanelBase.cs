using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelBase : MonoBehaviour {

    public bool OverideText = true;
    //Contains the index of the last selected slot (For reset)
    public int lastSelectedId = 0;
    
    /// <summary>
    /// Change the slots
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void Event_ItemSlotChanged(object sender, InventoryChangeEventsArgs e)
    {
        foreach (var slot in e.Slots)
        {
            ClearSlot(slot.Id);

            SetSlot(slot.FirstItem);
        }
    }

    /// <summary>
    /// Update the color of the slot border if selected
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void Event_ItemSelected(object sender, InventoryEventsArgs e)
    {
        SetBorderColor(lastSelectedId, Color.white);

        if (e.Item != null && e.Item.Slot != null)
        {
            SetBorderColor(e.Item.Slot.Id, Color.blue);
            lastSelectedId = e.Item.Slot.Id;
        }
    }

    /// <summary>
    /// If a Item is removed or added the UI must be updated
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void Event_ItemChanged(object sender, InventoryEventsArgs e)
    {
        SetSlot(e.Item);
    }

    /// <summary>
    /// Set the slot to the slot information
    /// </summary>
    /// <param name="item"></param>
    public void SetSlot(InventoryItem_Base item)
    {
        if (item == null || item.Slot == null)
            return;
        Transform slot = transform.GetChild(item.Slot.Id);

        Transform imageTransform = slot.Find("Border").Find("ItemImage");
        Transform textTransform = slot.Find("Border").Find("Text");
        Text HoverText = slot.Find("HoverMenu").Find("Text").GetComponent<Text>();
        Image image = imageTransform.GetComponent<Image>();
        Text txtCount = textTransform.GetComponent<Text>();

        int itemCount = item.Slot.Count;
        image.enabled = true;
        image.sprite = Resources.Load<Sprite>(item.Name + "Icon");
        HoverText.text = item.GetHoverMenue();

        if (itemCount == 0) //No more items in slot
        {
            image.enabled = false;
            image.sprite = null;
            HoverText.text = "";
        }
        else if (itemCount == 1) //First or last item in slot
        {

            if (OverideText) txtCount.text = "";
        }
        else
        {
            if (OverideText) txtCount.text = itemCount.ToString();
        }
    }

    /// <summary>
    /// Clears the complete slot
    /// </summary>
    /// <param name="item"></param>
    public void ClearSlot(int index)
    {
        Transform slot = transform.GetChild(index);

        Transform imageTransform = slot.Find("Border").Find("ItemImage");
        Transform textTransform = slot.Find("Border").Find("Text");
        Text HoverText = slot.Find("HoverMenu").Find("Text").GetComponent<Text>();
        Image image = imageTransform.GetComponent<Image>();
        Text txtCount = textTransform.GetComponent<Text>();

        image.enabled = false;
        image.sprite = null;
        HoverText.text = "";
        if(OverideText) txtCount.text = "";
    }
    
    /// <summary>
    /// Sets the Border color of a slot
    /// </summary>
    /// <param name="index"></param>
    /// <param name="color"></param>
    public void SetBorderColor(int index, Color color)
    {
        var buttonComponent = transform.GetChild(index).Find("Border").GetComponent<Button>();

        var colors = buttonComponent.colors;
        colors.normalColor = color;
        buttonComponent.colors = colors;
    }
}
