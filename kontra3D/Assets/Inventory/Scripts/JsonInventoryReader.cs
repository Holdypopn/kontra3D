using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonInventoryReader : MonoBehaviour
{
    [SerializeField]
    private static string path = Application.dataPath + @"\Inventory\Items.json";
   
    public static InventoryItems GetItems()
    {
        InventoryItems parsedData;
        using (StreamReader stream = new StreamReader(path))
        {
            string json = stream.ReadToEnd();
            parsedData = JsonUtility.FromJson<InventoryItems>(json);
        }

        Debug.Log(parsedData.Drink[0].Image);

        return parsedData;
    }
}