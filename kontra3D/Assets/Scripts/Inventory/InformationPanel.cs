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
            var buttonUse = transform.GetChild(3).GetComponent<Button>();
            var buttonRemove = transform.GetChild(4).GetComponent<Button>();
            var buttonEquip = transform.GetChild(5).GetComponent<Button>();


            description.text = "";
            image.sprite = null;

            buttonUse.gameObject.SetActive(false);
            buttonRemove.gameObject.SetActive(false);
            buttonEquip.gameObject.SetActive(false);
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
        var buttonUse = transform.GetChild(3).GetComponent<Button>();
        var buttonRemove = transform.GetChild(4).GetComponent<Button>();
        var buttonEquip = transform.GetChild(5).GetComponent<Button>();

        if (e.Item != null)
        {
            description.text = e.Item.Description;
            image.sprite = Resources.Load<Sprite>(e.Item.Name + "Icon");
            buttonUse.gameObject.SetActive(!(e.Item is InventoryItem_Equipment));
            buttonEquip.gameObject.SetActive(e.Item is InventoryItem_Equipment);
            buttonRemove.gameObject.SetActive(true);
        }
        else
        {
            description.text = "";
            image.sprite = null;
            buttonUse.gameObject.SetActive(false);
            buttonRemove.gameObject.SetActive(false);
            buttonEquip.gameObject.SetActive(false);

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

    /// <summary>
    /// When the button equip is pressed
    /// </summary>
    public void OnEquipClick()
    {
        ItemDragHandler.MoveFromInventoryToEquipment(SlotSelectHandler.CurrentSelectedInventoryTransform, Equipment.Instance.GetMatchingSlot(Inventory.Instance.GetSelectedItem()));
    }
}
