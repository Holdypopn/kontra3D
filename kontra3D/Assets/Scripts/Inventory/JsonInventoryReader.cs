using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonInventoryReader : MonoBehaviour
{
    [SerializeField]
    private static string path = Application.dataPath + @"\Resources\Items.json";
   
    public static InventoryItems GetItems()
    {
        InventoryItems parsedData;
        using (StreamReader stream = new StreamReader(path))
        {
            string json = stream.ReadToEnd();
            parsedData = JsonUtility.FromJson<InventoryItems>(json);
        }

        return parsedData;
    }
}