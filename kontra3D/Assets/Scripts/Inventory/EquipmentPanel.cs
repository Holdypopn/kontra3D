/// <summary>
/// Binded to the Equipment Panel
///     - Handels Slot updates
/// </summary>
public class EquipmentPanel : InventoryPanelBase
{
    /// <summary>
    /// Initialize all events of the inventory
    /// </summary>
    void Start()
    {
        Equipment.Instance.ItemAdded += Event_ItemChanged;
        Equipment.Instance.ItemRemoved += Event_ItemChanged;
        Equipment.Instance.ItemSelected += Event_ItemSelected;
        Equipment.Instance.ItemSlotChanged += Event_ItemSlotChanged;

        OverideText = false;

        Event_ItemSlotChanged(null, new InventoryChangeEventsArgs(Equipment.Instance.Slots));
    }
}
