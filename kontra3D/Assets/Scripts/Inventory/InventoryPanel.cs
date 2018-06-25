/// <summary>
/// Binded to the Inventory Panel
///     - Handels Slot updates
/// </summary>
public class InventoryPanel : InventoryPanelBase
{
    /// <summary>
    /// Initialize all events of the inventory
    /// </summary>
    void Start()
    {
        Inventory.Instance.ItemAdded += Event_ItemChanged;
        Inventory.Instance.ItemRemoved += Event_ItemChanged;
        Inventory.Instance.ItemSelected += Event_ItemSelected;
        Inventory.Instance.ItemSlotChanged += Event_ItemSlotChanged;
    }
}
