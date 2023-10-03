using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class Inventory
{
    // Define the items list as a static field to make it accessible to all instances of the class.
    private static List<string> items = new List<string>();

    public static void RemoveItem(string id)
    {
        bool removed = items.Remove(id);

        if (!removed)
        {
            throw new ArgumentException($"Element {id} is not in Inventory");
        }

        // Save the updated inventory to the file.
        SaveToFile("inventory.json");
    }

    public static void AddItem(string id)
    {
        items.Add(id);

        // Save the updated inventory to the file.
        SaveToFile("inventory.json");
    }

    public static List<string> GetItems()
    {
        return items;
    }

    public static void SaveToFile(string fileName)
    {
        string json = JsonConvert.SerializeObject(items);
        File.WriteAllText(fileName, json);
    }

    public static void LoadFromFile(string fileName)
    {
        if (File.Exists(fileName))
        {
            string json = File.ReadAllText(fileName);
            items = JsonConvert.DeserializeObject<List<string>>(json);
        }
        else
        {
            // Handle the case when the file doesn't exist, you can choose to do nothing or throw an exception.
            // For now, let's create an empty inventory.
            items = new List<string>();
        }
    }
}
