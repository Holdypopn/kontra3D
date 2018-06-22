using UnityEngine;

public class JsonInventoryReader : MonoBehaviour
{
    [SerializeField]
    private static string path = Application.dataPath + @"\Resources\Items.json";
   
    public static InventoryItems GetItems()
    {
        return JsonUtility.FromJson<InventoryItems>(Resources.Load<TextAsset>("Items").text);
    }
}