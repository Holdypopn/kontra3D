using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Binded to the Information Panel:
///     - Handling of use and remove item click 
///     - Handling of the update of the InformationPanel when new Item is selected
/// </summary>
public class InformationPanel : MonoBehaviour
{
    /// <summary>
    /// Subscribe to inventory events
    /// </summary>
    void Start()
    {
        Inventory.Instance.ItemSelected += InventoryScript_ItemSelected;
        Inventory.Instance.ItemRemoved += InventoryScript_ItemRemoved;
    }

    /// <summary>
    /// Update Information panel when item is removed from Inventory, if(removed == selected)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void InventoryScript_ItemRemoved(object sender, InventoryEventsArgs e)
    {
        if (Inventory.Instance.CurrentSelectedSlot == e.Item.Slot.Id && e.Item.Slot.Count == 0)
        {
            var image = transform.GetChild(1).GetComponent<Image>();
            var description = transform.GetChild(2).GetComponent<Text>();

            description.text = "";
            image.sprite = null;
        }
    }

    /// <summary>
    /// Update information panel when item is selected
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void InventoryScript_ItemSelected(object sender, InventoryEventsArgs e)
    {
        var image = transform.GetChild(1).GetComponent<Image>();
        var description = transform.GetChild(2).GetComponent<Text>();

        if (e.Item != null)
        {
            description.text = e.Item.Description;
            image.sprite = Resources.Load<Sprite>(e.Item.Name + "Icon");
        }
        else
        {
            description.text = "";
            image.sprite = null;
        }
    }

    /// <summary>
    ///  When the button "use" is clicked, the inventory gets informed
    /// </summary>
    public void OnUseClick()
    {
        Inventory.Instance.UseSelectedItem();
    }

    /// <summary>
    /// When the button "remove" is clicked, the inventory gets informed
    /// </summary>
    public void OnRemoveClick()
    {
        Inventory.Instance.RemoveSelectedItem();
    }
}
