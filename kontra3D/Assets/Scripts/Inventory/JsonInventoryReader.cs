using UnityEngine;

public class JsonInventoryReader : MonoBehaviour
{   
    public static InventoryItems GetItems()
    {
        return JsonUtility.FromJson<InventoryItems>(Resources.Load<TextAsset>("Items").text);
    }
}